using HandlerReq;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HandlerReq
{
    public class WebSocketHandler
    {
        public static async Task HandleWebSocket(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            using var socket = await context.WebSockets.AcceptWebSocketAsync();
            string token = context.Request.Query["token"];
            Console.WriteLine($"Received token: {token}");

            var buffer = new byte[1024 * 4];
            var segment = new ArraySegment<byte>(buffer);

            while (true)
            {
                var result = await socket.ReceiveAsync(segment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by the WebSocketHandler", CancellationToken.None);
                    break;
                }

                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message: {message}");

                try
                {
                    using (JsonDocument doc = JsonDocument.Parse(message))
                    {
                        JsonElement root = doc.RootElement;

                        if (root.TryGetProperty("type", out JsonElement typeElement))
                        {
                            string type = typeElement.GetString();
                            Console.WriteLine($"Type: {type}");
                            string user = root.TryGetProperty("user", out JsonElement userElement) ? userElement.GetString() : "Unknown";
                            string password = root.TryGetProperty("password", out JsonElement passwordElement) ? passwordElement.GetString() : "Unknown";

                            Console.WriteLine($"User: {user}");
                            Console.WriteLine($"Password: {password}");

                            IApiFunc endpoint = type switch
                            {
                                "Language" => new EndpointLanguage(socket),
                                "SSN" => new EndpointSSN(socket),
                                "Card" => new EndpointCard(socket),
                                "Amount" => new EndpointAmount(socket),
                                "Fees" => new EndpointFees(socket),
                                "ConfirmAmount" => new EndpointConfirmAmount(socket),
                                "Signature" => new EndpointSignature(socket),
                                "Receipt" => new EndpointReceipt(socket),
                                _ => null
                            };

                            if (endpoint != null)
                            {
                                endpoint.SendResponse();
                            }

                            var Login = new EndpointLogin();

                            if (type == "Login")
                            {
                                Login.SendResponse(user, password, socket);

                            }
                            //else if (type == "LogOut")
                            //{
                            //    await logout.SendResponse(user, password, socket);
                            //}
                            else if (type == "SendSearchinfo")
                            {
                                if (root.TryGetProperty("patroninformation", out JsonElement patronElement))
                                {
                                    var patronInfo = patronElement.GetRawText();
                                    //below commented line is for Cash Advance service API

                                    //var patroninformation = new Patroninfo();
                                    
                                    //this is for CAPS API
                                    var patroninformation = new Patroninfocaps();

                                    patroninformation.Patroninformation(patronInfo, socket);
                                }
                                else
                                {

                                    Console.WriteLine("Patron information not found.");
                                }
                            }

                            else if (type == "Patroninformation")
                            {
                                if (root.TryGetProperty("patroninformation", out JsonElement patronElement))
                                {
                                    var patronInfo = patronElement.GetRawText();
                                    var patronentity = new PatronAddiCaps();
                                    Console.WriteLine(patronInfo);
                                    patronentity.Patroninformation(patronInfo, socket);
                                    Console.WriteLine($"SuccessFully added patron data");
                                }
                            }
                            else
                            {
                                //Console.WriteLine("Type property not found.");
                            }
                        }
                    }
                }
                catch (JsonException e)
                {
                    Console.WriteLine($"Error parsing JSON: {e.Message}");
                }
            }
        }
    }
}
