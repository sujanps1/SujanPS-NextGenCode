using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using HandlerReq;

namespace HandlerReq
{
    public class EndpointFees : IApiFunc
    {
        private readonly WebSocket _webSocket;

        public EndpointFees(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void SendResponse()
        {
            Console.WriteLine("Please enter the fees amount:");

            string feesAmount = Console.ReadLine();

            Console.WriteLine($"Entered Fees Amount: {feesAmount}");
            string message = $"FEES:{feesAmount}";

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
        }
    }
}
