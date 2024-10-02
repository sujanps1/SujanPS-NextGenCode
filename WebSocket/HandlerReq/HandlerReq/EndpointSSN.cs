using System.Net.WebSockets;
using System.Text;
using HandlerReq;
using System;
using System.Threading;

namespace HandlerReq
{
    public class EndpointSSN : IApiFunc
    {
        private readonly WebSocket _webSocket;

        public EndpointSSN(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void SendResponse()
        {
            string decision = null;

            while (decision != "yes" && decision != "no")
            {
                Console.WriteLine("Do you want to send the SSN data? (yes/no):");
                decision = Console.ReadLine()?.Trim().ToLower();

                if (decision == "yes")
                {
                    string SSN = null;

                    while (SSN == null || SSN.Length != 10 || !long.TryParse(SSN, out _))
                    {
                        Console.WriteLine("Please enter a 10-digit number:");
                        SSN = Console.ReadLine()?.Trim();

                        if (SSN.Length == 10 && long.TryParse(SSN, out _))
                        {
                            Console.WriteLine($"SSN Number is: {SSN}");

                            string message = $"SSN:{SSN}";
                            byte[] buffer = Encoding.UTF8.GetBytes(message);
                            _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid 10-digit number.");
                        }
                    }
                }
                else if (decision == "no")
                {
                    string msg = "No SSN data from the server";
                    string noMsg = $"NOSSN:{msg}";
                    byte[] buffer = Encoding.UTF8.GetBytes(noMsg);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 'yes' or 'no' only.");
                }
            }
        }
    }
}
