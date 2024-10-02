using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebTrasportProtocol;
using EviCapsConnect.Entities;
using System.Transactions;
using WebTrasportProtocol;

namespace WebTrasportProtocol
{
    public class PatronAddiCaps
    {
        public async Task Patroninformation(string requestJson, Stream transportStream, JsonElement root)
        {

            if (requestJson != null)
            {
                JObject request = JObject.Parse(requestJson);
                Console.WriteLine($"This is objec {request["firstName"]}");
                Console.WriteLine($"this is date {request["dateOfBirth"]}");
                DateTime birthDate = DateTime.Parse(request["dateOfBirth"].ToString());
                string formattedDateString = birthDate.ToString("M/d/yyyy");

                Identity id = new Identity
                {
                    Type = (string)request["type"],
                    Number = (string)request["number"],
                    State = (string)request["state"],
                    Country = (string)request["country"],
                    ExpiryDate = "",
                    IdReadMethod = ""
                };
                Address address = new Address
                {
                    AddressLine1 = (string)request["addressLine1"],
                    AddressLine2 = (string)request["addressLine2"],
                    City = (string)request["city"],
                    State = (string)request["state"],
                    Zip = (string)request["zip"],
                    Country = (string)request["country"]
                };
                Contact contact = new Contact
                {
                    Phone = (string)request["phone"],
                    Email = (string)request["email"]
                };


                Transaction transaction = new Transaction
                {
                    TransactionSource = "CC",
                    PlayerCardNumber = (string)request["payercardno"],
                    PatronId = 0,
                    FirstName = (string)request["firstName"],
                    MiddleName = (string)request["middleName"],
                    LastName = (string)request["lastName"],
                    DateOfBirth = formattedDateString,
                    Ssn = (string)request["ssn"],
                    Identity = id,
                    Address = address,
                    Contact = contact,
                    PerformKYC = false,
                    ModifyBy = "ccdaniel",
                    EncryptionType = 0,
                    Occupation = (string)request["occupation"]
                };

                string jsonString = System.Text.Json.JsonSerializer.Serialize(transaction, new JsonSerializerOptions { WriteIndented = true });
                ConnectorClass connectorClass = new ConnectorClass("NVGCAC20", "DAXQ4981ZT366519RZMR");

                string response = connectorClass.RequestCallBack("terminal/v1/NVGCAC20/patrons", jsonString);
                JObject data = JObject.Parse(response);


                JObject responseBody = (JObject)data["responseBody"];
                Console.WriteLine($"Response from RequestCallBack is {responseBody["patronId"]}");


                if (responseBody.ContainsKey("patronId") && !string.IsNullOrEmpty(responseBody["patronId"].ToString()))
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