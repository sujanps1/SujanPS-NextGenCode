using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace WebTrasportProtocol
{
    public class EndpointSignature : IApiFunc
    {
        public async Task SendResponseAsync(Stream transportStream, JsonElement root)
        {
            Console.WriteLine("Please provide your signature:");

            string signature = Console.ReadLine();

            Console.WriteLine($"Signature: {signature}");

            string message = $"SIGNATURE:{signature}";
            var responseBytes = Encoding.UTF8.GetBytes(message);
            await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            await transportStream.FlushAsync();
        }
    }

}
