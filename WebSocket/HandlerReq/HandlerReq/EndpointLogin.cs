using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HandlerReq;
using Newtonsoft.Json.Linq;

namespace HandlerReq
{
    class EndpointLogin
    {
        public async Task SendResponse(string user, string password, WebSocket socket)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://10.208.152.87:3003/cash-access/terminal/v1/NVGCAC50/login";
                    string json = $"{{\"transactionSource\":\"C\",\"password\":\"{password}\",\"userName\":\"{user}\"}}";

                    Console.WriteLine($"Request JSON: {json}");

                    ConnectorClass connectorClass = new ConnectorClass("NVGCAC20", "DAXQ4981ZT366519RZMR");
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
                        string info = $@"{{""type"": ""Login"",""body"": ""true""}}";
                        string dataToSend = $"LOGIN:true";

                        var bytes = Encoding.UTF8.GetBytes(dataToSend);
                        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                        await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else if ((string)data["status"] == "false")
                    {
                        //string info = $@"{{""type"": ""Login"",""body"": ""{responseBody}""}}";
                        string dataToSend = $"LOGIN:{responseBody}";

                        var bytes = Encoding.UTF8.GetBytes(dataToSend);
                        var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                        await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
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
