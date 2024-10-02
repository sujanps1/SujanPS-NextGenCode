using System.Text.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.WebSockets;

namespace WebTrasportProtocol
{
    public class EndpointLogin
    {
        public async Task SendResponse(string user, string password, Stream transportStream, JsonElement root)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://10.208.152.87:3003/cash-access/terminal/v1/NVGCAC50/login";
                    string json = $"{{\"transactionSource\":\"C\",\"password\":\"{password}\",\"userName\":\"{user}\"}}";

                    Console.WriteLine($"Request JSON: {json}");

                    string securityKey = "DAXQ4981ZT366519RZMR";
                    ConnectorClass connectorClass = new ConnectorClass("NVGCAC20", securityKey);

            

                    var response = connectorClass.RequestCallBack("terminal/v1/NVGCAC20/login", json);

                    if (string.IsNullOrWhiteSpace(response))
                    {
                        Console.WriteLine("Received empty or null response.");
                        return;
                    }

                    JObject data;
                    try
                    {
                        data = JObject.Parse(response);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing JSON: {ex.Message}");
                        return;
                    }

                    //Console.WriteLine($"Response from RequestCallBack: {data}");

                    string responseBody = (string)data["responseBody"]["description"] ?? "No description available";
                    Console.WriteLine(responseBody);
                    if ((string)data["status"] == "true")
                    {
                        string dataToSend = $"LOGIN:true";

                        var responseBytes = Encoding.UTF8.GetBytes(dataToSend);
                        await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await transportStream.FlushAsync();
                    }
                    else if ((string)data["status"] == "false")
                    {
                        string dataToSend = $"LOGIN:{responseBody}";

                        var responseBytes = Encoding.UTF8.GetBytes(dataToSend);
                        await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await transportStream.FlushAsync();
                    }
                    else
                    {
                        Console.WriteLine("Unexpected status value in response.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendResponse: {ex.Message}");
            }
        }
    }
}
