using EviCapsConnect.Entities;
using EviCapsConnect;
using EviCapsConnect.Processors;
using GCA.CashClub.Library;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Configuration;
using static EviCapsConnect.WebHelper;
using System.Globalization;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Web;
using CAPSUtility.service;
using Everi.CAPS.Utility.Encryption;
using WebTrasportProtocol;

namespace WebTrasportProtocol
{

    public class ConnectorClass
    {
        CAPSCommunicationAdapter _caps;
        private string _terminalID;
        private string _securityKey;
        public ConnectorClass(string TerminalID, string SecurityKey)
        {
            _caps = CAPSCommunicationAdapter.GetInstance("http://10.208.152.87:3003/cash-access");
            _terminalID = TerminalID;
            _securityKey = SecurityKey;
        }

        public string RequestCallBack(string APIEndPoint, string Request)
        {
            string text3 = default(string);
            string stRresponse = default(string);
            try
            {
                Communictor communictor = new Communictor();


                stRresponse = communictor.SendReceiveOverHttp(EviCapsConnect.HttpVerb.POST, APIEndPoint, Request, _terminalID, _securityKey);
                stRresponse = $@"{{""status"":""true"",""responseBody"":{stRresponse}}}";


            }
            catch (ApplicationException ex)

            {
                stRresponse = $@"{{""status"":""false"",""responseBody"":{ex.Message}}}";
            }

            return stRresponse;
        }
    }


    public class Communictor
    {
        public Communictor()
        {
        }

        public string SendReceiveOverHttp(HttpVerb verb, string strMethod, string requestMessage, string TerminalID, string SecurityKey, bool isRetriedTransaction = false)
        {
            string EndPoint = "http://10.208.152.87:3003/cash-access";

            string result = null;
            string text = EndPoint;
            byte[] bytes = Encoding.UTF8.GetBytes(requestMessage);
            if (!string.IsNullOrEmpty(strMethod))
            {
                text = $"{EndPoint}/{strMethod}";
            }

            if (verb == HttpVerb.GET)
            {
                UriBuilder uriBuilder = new UriBuilder(text);
                NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
                nameValueCollection["transactionSource"] = "C";
                uriBuilder.Query = nameValueCollection.ToString();
                text = uriBuilder.ToString();
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = verb.ToString();
            httpWebRequest.ContentLength = bytes.Length;
            RequestHeader requestHeader = ConstructSignedKey(SecurityKey, TerminalID);
            httpWebRequest.Headers.Add("x-evri-requestdate", requestHeader.currentUTCDate);
            httpWebRequest.Headers.Add("x-evri-service", requestHeader.serviceName);
            httpWebRequest.Headers.Add("x-evri-signedkey", requestHeader.SignedKey);
            Console.WriteLine(requestHeader.SignedKey);
            httpWebRequest.Headers.Add("terminal-id", requestHeader.terminalid);
            if (bytes.Length != 0 && (httpWebRequest.Method == HttpVerb.POST.ToString() || httpWebRequest.Method == HttpVerb.PUT.ToString()))
            {
                using Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                using HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string text2 = string.Empty;
                using (Stream stream2 = httpWebResponse.GetResponseStream())
                {
                    if (stream2 != null)
                    {
                        using StreamReader streamReader = new StreamReader(stream2);
                        text2 = streamReader.ReadToEnd();
                        Console.WriteLine(text2);
                    }
                }

                if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    string message = $"Request failed. Received HTTP {httpWebResponse.StatusCode}";
                    throw new ApplicationException(message);
                }

                result = text2;
            }
            catch (WebException ex)
            {
                string text3 = default(string);

                if (isRetriedTransaction)
                {
                    throw;
                }

                try
                {
                    text3 = new StreamReader(ex.Response?.GetResponseStream())?.ReadToEnd();

                    Console.WriteLine(text3);
                    if (!string.IsNullOrEmpty(text3))
                    {
                        ex.Response.GetResponseStream().Position = 0L;
                    }

                    ExceptionStatus exceptionStatus = Utility.ConvertJsonToObject<ExceptionStatus>(text3.ToString());
                    if (exceptionStatus.Code == "02")
                    {
                        DateTime.TryParseExact(exceptionStatus.ResponseDate, "yyyyMMddTHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result2);
                        result = SendReceiveOverHttp(verb, strMethod, requestMessage, TerminalID, SecurityKey, isRetriedTransaction: true);
                    }
                    else
                    {
                        if (!strMethod.Contains("countersigning-reset"))
                        {
                            throw;
                        }

                        result = text3;
                        Console.WriteLine(result);
                    }
                }
                catch
                {
                    throw new ApplicationException(text3 ?? ex.Message, ex);
                }
            }

            return result;
        }
        private RequestHeader ConstructSignedKey(string securityKey, string terminalid)
        {

            RequestHeader requestHeader = new RequestHeader();
            ISigning val = (ISigning)APIFactory.getInstance("SIGNING");
            requestHeader.serviceName = "CC";
            DateTime getOffsetTime = DateTime.UtcNow;
            requestHeader.currentUTCDate = getOffsetTime.ToString("yyyyMMddTHHmmss");
            requestHeader.SignedKey = val.HashRequest(securityKey, getOffsetTime, requestHeader.serviceName);
            requestHeader.terminalid = terminalid;
            return requestHeader;
        }
    }

}
