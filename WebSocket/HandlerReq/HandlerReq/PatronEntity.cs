using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HandlerReq;
using Newtonsoft.Json.Linq;

namespace HandlerReq
{
    public class PatronEntity
    {
        public async Task Patroninfo(string requestBody, WebSocket socket, string token)
        {
            try
            {
                var getResponseAsync = new GetResponseAsync();
                string responseBody = await getResponseAsync.GetResponse(token, requestBody);
                Console.WriteLine(responseBody);
                await LoadDataFormHostResponseToEntity(responseBody, socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        private async Task LoadDataFormHostResponseToEntity(string hostResponse, WebSocket socket)
        {
            try
            {
                var data = JObject.Parse(hostResponse);
                var occuranceArray = data["Occurance"] as JArray;
                var informationArray = data["Information"] as JArray;
                var playerCardDetailsArray = data["PlayerCardDetails"] as JArray;

                if (informationArray == null || informationArray.Count == 0)
                {
                    string dataToSends = $"Nopatrnfound:{"Nopatronfound"}";

                    var bytess = Encoding.UTF8.GetBytes(dataToSends);
                    var arraySegments = new ArraySegment<byte>(bytess, 0, bytess.Length);
                    await socket.SendAsync(arraySegments, WebSocketMessageType.Text, true, CancellationToken.None);
                    return;
                }

                var firstInformation = informationArray[0] as JObject;
                var playerCardDetail = playerCardDetailsArray?[0] as JObject;
                Console.WriteLine($"this is player car detail{playerCardDetail}");

                string patronId = (string)firstInformation["PatronId"];
                string birthDate = (string)firstInformation["BirthDate"];
                string ssn = (string)firstInformation["SSN"];
                string ssnLastFour = (string)firstInformation["SSNLastFour"];
                string ssnStatus = (string)firstInformation["SSNStatus"];
                bool optOut = (bool)firstInformation["OptOut"];
                bool optInExpired = (bool)firstInformation["OptInExpired"];
                string optIn = (string)firstInformation["OptIn"];
                string fullName = (string)firstInformation["FullName"];
                string givenName = (string)firstInformation["GivenName"];
                string middleName = (string)firstInformation["MiddleName"];
                string lastName = (string)firstInformation["FamilyName"];
                string postalCode = (string)firstInformation["PostalCode"];
                string street = (string)firstInformation["Street"];
                string city = (string)firstInformation["City"];
                string aptSte = (string)firstInformation["AptSte"];
                string phoneNumber = (string)firstInformation["SubscriberNumber"];
                string emailAddress = (string)firstInformation["EmailAddress"];
                string occupation = (string)firstInformation["Occupation"];
                string ssnAgain = (string)firstInformation["SSN"];

                string playerCardNumber = (string)playerCardDetail["PlayerCardNumber"];
                Console.WriteLine(playerCardNumber);

                string swipedPlayerCardNumber = playerCardDetail?["SwipedPlayerCardNumber"]?.ToString();

                var json = $@"{{
                        ""PatronId"": ""{patronId}"",
                        ""BirthDate"": ""{birthDate}"",
                        ""SSN"": ""{ssn}"",
                        ""SSNLastFour"": ""{ssnLastFour}"",
                        ""SSNStatus"": ""{ssnStatus}"",
                        ""OptOut"": {optOut.ToString().ToLower()},
                        ""OptInExpired"": {optInExpired.ToString().ToLower()},
                        ""OptIn"": ""{optIn}"",
                        ""FullName"": ""{fullName}"",
                        ""GivenName"": ""{givenName}"",
                        ""MiddleName"": ""{middleName}"",
                        ""LastName"": ""{lastName}"",
                        ""PostalCode"": ""{postalCode}"",
                        ""Street"": ""{street}"",
                        ""City"": ""{city}"",
                        ""AptSte"": ""{aptSte}"",
                        ""PhoneNumber"": ""{phoneNumber}"",
                        ""EmailAddress"": ""{emailAddress}"",
                        ""Occupation"": ""{occupation}"",
                        ""SSNAgain"": ""{ssnAgain}"",
                        ""PlayerCardNumber"": ""{playerCardNumber}"",
                        ""SwipedPlayerCardNumber"": ""{swipedPlayerCardNumber}""
                }}";
                Console.WriteLine(json);

                string dataToSend = $"PATRON:{json}";

                var bytes = Encoding.UTF8.GetBytes(dataToSend);
                var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);

                await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
