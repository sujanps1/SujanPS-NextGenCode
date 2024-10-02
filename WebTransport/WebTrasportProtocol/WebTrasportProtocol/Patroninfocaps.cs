using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using WebTrasportProtocol;
using EviCapsConnect.Entities;
using WebTrasportProtocol;
using System.Text.Json;

namespace WebTrasportProtocol
{
    public class Patroninfocaps
    {
        public async Task Patroninformation(string patronInfoJson, Stream transportStream, JsonElement root)
        {
            var patronInfo = (JObject)JsonConvert.DeserializeObject(patronInfoJson);

            if (patronInfo != null)
            {
                Console.WriteLine($"this is date {patronInfo["BirthDate"]}");
                DateTime birthDate = DateTime.Parse(patronInfo["BirthDate"].ToString());
                string formattedDateString = birthDate.ToString("M/d/yyyy");

                var idbody = new identity
                {

                    type = patronInfo["IDType"].ToString(),
                    number = patronInfo["IDNumber"].ToString(),
                    state = patronInfo["IDState"].ToString(),
                    country = patronInfo["IDCountry"].ToString(),

                };
                var requestInfo = new PatronRequestCaps
                {
                    transactionSource = "CC",
                    patronId = "",
                    playerCardNumber = "",
                    identity = idbody,
                    isPrivacy = true,
                    isMarketing = true,
                    eCheckDetails = true,
                    dob = formattedDateString,
                    additionalPatronDetails = true,
                    fetchFlexChargeTC = true

                };

                string requestJson = JsonConvert.SerializeObject(requestInfo, Formatting.Indented);

                Console.WriteLine(requestJson);
                ConnectorClass connectorClass = new ConnectorClass("NVGCAC20", "DAXQ4981ZT366519RZMR");

                string response = connectorClass.RequestCallBack("terminal/v1/NVGCAC20/patrons/patronLookup", requestJson);
                JObject data = JObject.Parse(response);


                Console.WriteLine($"This is patron id from caps {data["responseBody"]["patronId"]}");


                if (!string.IsNullOrEmpty((string)data["responseBody"]["patronId"]))
                {

                    string dataToSend = $"PATRON:{data["responseBody"]}";


                    var responseBytess = Encoding.UTF8.GetBytes(dataToSend);
                    await transportStream.WriteAsync(responseBytess, 0, responseBytess.Length);
                    await transportStream.FlushAsync();
                }
                else
                {
                    string dataToSends = $"Nopatrnfound:{"Nopatronfound"}";

                    var responseBytess = Encoding.UTF8.GetBytes(dataToSends);
                    await transportStream.WriteAsync(responseBytess, 0, responseBytess.Length);
                    await transportStream.FlushAsync();
                }


            }
            else
            {

                Console.WriteLine($"error");
            }
        }
    }
}