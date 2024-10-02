using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace HandlerReq
{
    public class EndpointAmount : IApiFunc
    {
        private readonly WebSocket _webSocket;

        public EndpointAmount(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void SendResponse()
        {
            string amount = null;

            while (true)
            {
                Console.WriteLine("Please enter the amount in dollars (e.g., 100 or 99.99):");

                amount = Console.ReadLine();

                if (decimal.TryParse(amount, out decimal parsedAmount) && parsedAmount >= 0)
                {
                    Console.WriteLine($"Entered Amount: {amount}");

                    string message = $"AMOUNT:{amount}";
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount in dollars.");
                }
            }
        }
    }
}
