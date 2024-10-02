using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebTrasportProtocol;

namespace WebTrasportProtocol

{
    public class GetResponseAsync
    {
        private TimeSpan OffsetTime;
        public async Task<string> GetResponse(string Token, string RequestBody, IDictionary<string, string> optionalHeaders = default(IDictionary<string, string>), bool isReauthenticate = false)
        {
            const string MethodName = "GetResponse(Token:{0})";

            string Result = string.Empty;

            byte[] byteInputSignature;

            byteInputSignature = Encoding.UTF8.GetBytes(RequestBody);

            try
            {
                Result = doProcess(Token, byteInputSignature, optionalHeaders);
            }
            catch (WebException webex)
            {
                if (webex.Status == WebExceptionStatus.Timeout || webex.Status == WebExceptionStatus.ConnectFailure)
                {
                    CodeHelper.LogErrorToMarsAndClient(webex.InnerException, MARSMessages.FailedToConnectInternet, string.Format(MethodName, Token), true);
                }
                else
                {
                    CodeHelper.LogErrorToMarsAndClient(webex.InnerException, MARSMessages.FailedToConnectInternet, string.Format(MethodName, Token), false);
                }

                throw webex;
            }
            catch (Exception ex)
            {
                if (isReauthenticate)
                    throw ex;

                ErrorMessageEntity objErrorEntity = new ErrorMessageEntity();

                try
                {
                }
                catch (Newtonsoft.Json.JsonException)
                {
                    throw;
                }

                if (objErrorEntity?.ErrorCode == "5002")
                {
                    if (DateTime.TryParseExact(objErrorEntity.ServerTime.ToString(), "yyyyMMddTHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime serverTime))
                    {
                        OffsetTime = serverTime - DateTime.UtcNow;
                        Result = await GetResponse(Token, RequestBody, isReauthenticate: true);
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to parse server time.");
                    }
                }
                else
                {
                    throw;
                }
            }

            return Result;
        }
        private DateTime GetOffsetTime
        {
            get
            {
                return DateTime.UtcNow.Add(OffsetTime);
            }
        }
        private string doProcess(string Token, byte[] byteSignature, IDictionary<string, string> optionalHeaders = default(IDictionary<string, string>))
        {

            string URL = string.Concat("http://10.140.129.54/CashClubVanillaWebHost/CageWebService.svc", "/doprocess");

            Uri uri = new Uri(URL);

            var request = (HttpWebRequest)WebRequest.Create(uri);

            WebResponse response;

            request.ContentLength = byteSignature.Length;
            string RequestToken = "REQUEST_TOKEN";
            string Securitykey = "SECURITY_KEY";
            string TerminalID = "TERMINAL_ID";
            string Region = "REGION";
            string ProductName = "PRODUCT_NAME";
            string RequestDate = "x-evri-requestdate";
            string ServiceName = "x-evri-service";
            string SignedKey = "x-evri-signedkey";
            string DateTimeFormat = "yyyyMMddTHHmmss";

            request.Headers.Add(RequestToken, Token);

            request.Headers.Add(Securitykey, "E+gdW+ibrEM4T78WQelR7DU/p3Ul7u93dZv6SXfk9PA=");

            request.Headers.Add(TerminalID, "NVGCAC05");


            request.Headers.Add(Region, "ConnectionString");

            request.Headers.Add(ProductName, "CashClub");


            var encryptDecrypt = new EncryptDecrypt();
            string _securityKey = "E+gdW+ibrEM4T78WQelR7DU/p3Ul7u93dZv6SXfk9PA=";
            EviCapsConnect.WebHelper.RequestHeader requestHeader =
              EviCapsConnect.HostHelper.ConstructSignedKey(encryptDecrypt.EncryptDecryptData(_securityKey, false), "NVGCAC05", GetOffsetTime, "CC");

            request.Headers.Add(RequestDate, requestHeader.currentUTCDate);

            request.Headers.Add(ServiceName, requestHeader.serviceName);

            request.Headers.Add(SignedKey, requestHeader.SignedKey);

            string versionData = ApplicationConfiguration.Instance.VersionInfo.Replace(".", "");

            request.Headers.Add("Version", versionData);

            if (optionalHeaders != null)
            {
                if (optionalHeaders.Count != 0)
                {
                    foreach (KeyValuePair<string, string> optionalHeader in optionalHeaders)
                    {
                        request.Headers.Add(optionalHeader.Key, optionalHeader.Value);
                    }
                }
            }


            request.Method = "POST";

            Stream dataStream = default(Stream);

            try
            {
                dataStream = request.GetRequestStream();

                dataStream.Write(byteSignature, 0, byteSignature.Length);

                dataStream.Close();
            }
            catch (Exception ex)
            {
                if (((WebException)ex).Status == WebExceptionStatus.Timeout || ((WebException)ex).Status == WebExceptionStatus.ConnectFailure)
                {

                }
                else
                {
                }
            }

            string strResponse = string.Empty;
            try
            {
                response = request.GetResponse();

                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                strResponse = reader.ReadToEnd();


                dataStream.Close();

                reader.Close();
            }
            catch (WebException webException)
            {
                StreamReader reader;

                if (webException.Status == WebExceptionStatus.Timeout)
                {

                }
                else
                {
                    try
                    {
                        dataStream = webException.Response.GetResponseStream();

                        reader = new StreamReader(dataStream);

                        strResponse = reader.ReadToEnd();

                    }
                    catch (Exception ex)
                    {
                        if (request.Proxy != default(WebProxy))
                        {

                        }
                        else
                        {
                            throw new WebException(MARSMessages.FailedToConnectInternet, ex);
                        }
                    }

                    ErrorMessageEntity objErrorEntity;

                    try
                    {
                        objErrorEntity = new ErrorMessageEntity();

                    }
                    catch (Exception)
                    {

                    }

                    dataStream.Close();


                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("strResponse", "Error Occured" + strResponse);

            }

            strResponse = ValidateResponse(strResponse);

            return strResponse;
        }

        internal const string ResponseBody = "ResponseBody";
        internal const string ResponseStatus = "ResponseStatus";
        internal const string ResponseFailure = "FAILURE";
        internal const string Status = "Status";
        internal const string Success = "SUCCESS";

        public string ValidateResponse(string response)
        {
            string responseBody = string.Empty;
            string status = string.Empty;

            var data = (JObject)JsonConvert.DeserializeObject(response);

            if (data == null)
            {
                throw new ArgumentException("Response is not a valid JSON");
            }

            var responseStatusObject = data["ResponseStatus"];
            var responseBodyObject = data["ResponseBody"];

            if (responseStatusObject == null)
            {
                throw new ArgumentException("ResponseStatus key not found or is not an object in JSON");
            }

            if (responseBodyObject == null)
            {
                throw new ArgumentException("ResponseBody key not found or is not an object in JSON");
            }

            var statusToken = responseStatusObject["Status"];
            status = statusToken?.ToString();

            responseBody = responseBodyObject.ToString();

            if (status.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
            {
                return responseBody;
            }
            else
            {
                throw new Exception(responseBody);
            }
        }


    }

    public class ErrorMessageEntity
    {
        public string ErrorCode { get; set; }
        public DateTime ServerTime { get; set; }
    }

    public static class CodeHelper
    {
        public static void LogErrorToMarsAndClient(Exception ex, string message, string methodName, bool isCritical)
        {
        }
    }

    public static class MARSMessages
    {
        public const string FailedToConnectInternet = "Failed to connect to the internet.";
    }

}
