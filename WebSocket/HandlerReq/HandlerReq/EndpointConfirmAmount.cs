using HandlerReq;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace HandlerReq
{
    public class EndpointConfirmAmount : IApiFunc
    {
        private readonly WebSocket _webSocket;

        public EndpointConfirmAmount(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void SendResponse()
        {
            string confirm = null;

            while (true)
            {
                Console.WriteLine("Do you want to confirm the amount? (yes/no):");

                confirm = Console.ReadLine()?.Trim().ToLower();

                if (confirm == "yes")
                {
                    Console.WriteLine("Confirmed Payment..!");
                    string message = $"CONFIRMAMOUNT";
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                    break; 
                }
                else if (confirm == "no")
                {
                    Console.WriteLine("Payment not confirmed.");
                    string msg = "No payment confirmation from the server";
                    string noMsg = $"NOPAYMENT:{msg}";
                    byte[] buffer = Encoding.UTF8.GetBytes(noMsg);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                    break; 
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            }
        }
    }
}
