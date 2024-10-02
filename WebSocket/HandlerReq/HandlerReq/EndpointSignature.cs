using HandlerReq;
using System;
using System.Net.WebSockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;

namespace HandlerReq
{
    public class EndpointSignature : IApiFunc
    {
        private readonly WebSocket _webSocket;

        public EndpointSignature(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void SendResponse()
        {
            Console.WriteLine("Please provide your signature:");

            string signature = Console.ReadLine();

            Console.WriteLine($"Signature: {signature}");
            
            string message = $"SIGNATURE:{signature}";

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
        }
    }
}
