using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using HandlerReq;

namespace HandlerReq
{
    public class Patroninfo
    {
        public async Task Patroninformation(string patronInfoJson, WebSocket socket)
        {
            // Deserialize JSON string into JObject
            var patronInfo = (JObject)JsonConvert.DeserializeObject(patronInfoJson);

            if (patronInfo != null)
            {
                // Parse dates correctly(
                DateTime idExpiryDate = DateTime.Parse(patronInfo["IDExpiryDate"].ToString());
                DateTime birthDate = DateTime.Parse(patronInfo["BirthDate"].ToString());

                Console.WriteLine(patronInfo);

                // Format dates to the desired ISO 8601 format
                string requestBody = $@"
                {{
                     ""RequestInfo"": {{""VersionInfo"": ""6.7.0.0"",""BuildNumber"": ""6.6.8.0.20230725"",""SecurityKey"": ""E+gdW+ibrEM4T78WQelR7DU/p3Ul7u93dZv6SXfk9PA="",""TerminalID"": ""NVGCAC05"",""MerchantSID"": ""334"",""Region"": ""NorthAmerica"",""ProductName"":""CashClub"",""Operator"": ""cv_anil"",""SequenceNumber"": ""00000101"",""RequestToken"": ""PS002""
                 }},""RequestBody"": {{
                 ""IDCountry"": ""{patronInfo["IDCountry"]}"",
                        ""IDState"": ""{patronInfo["IDState"]}"",
                        ""IDType"": ""{patronInfo["IDType"]}"",
                        ""IDNumber"": ""{patronInfo["IDNumber"]}"",
                        ""IDExpiryDate"": ""{idExpiryDate:yyyy-MM-ddTHH:mm:ss}"",
                        ""BirthDate"": ""{birthDate:yyyy-MM-ddTHH:mm:ss}"",
                        ""ExpiryDateChecked"": ""false"",
                        ""Last4SSN"": null,
                        ""SkipLast4SSNSearch"": false,
                        ""TerminalId"": ""NVGCAC05"",
                        ""PreferredLanguage"": null
                    }}
                }}";
                var patronentity = new PatronEntity();
                patronentity.Patroninfo(requestBody, socket, "PS002");




            }
        }
    }
}
