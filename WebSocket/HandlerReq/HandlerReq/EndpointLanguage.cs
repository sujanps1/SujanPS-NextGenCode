using HandlerReq;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace HandlerReq
{
    public class EndpointLanguage : IApiFunc
    {
        private readonly WebSocket _webSocket;

        public EndpointLanguage(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void SendResponse()
        {
            string language = null;

            string[] allowedLanguages = { "English", "French", "Greek", "Hindi" };

            while (true)
            {
                Console.WriteLine("Please select a language: English, French, Greek, or Hindi:");

                language = Console.ReadLine()?.Trim();

                if (Array.Exists(allowedLanguages, lang => lang.Equals(language, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Selected Language: {language}");

                    string message = $"LANGUAGE:{language}";
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter one of the following: English, French, Greek, or Hindi.");
                }
            }
        }
    }
}
