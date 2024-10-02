using System.Text;
using System.Text.Json;
using System.IO;
using WebTrasportProtocol;

namespace WebTransportProtocol
{
    public class EndpointLanguage : IApiFunc
    {
        public async Task SendResponseAsync(Stream transportStream, JsonElement root)
        {
            string[] validLanguages = { "English", "Hindi", "French", "Greek" };
            string language = "";

            while (true)
            {
                Console.WriteLine("Please enter a language (English, Hindi, French, Greek):");
                language = Console.ReadLine()?.Trim();

                if (Array.Exists(validLanguages, lang => lang.Equals(language, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Selected Language: {language}");
                    string message = $"LANGUAGE:{language}";
                    var responseBytes = Encoding.UTF8.GetBytes(message);
                    await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    await transportStream.FlushAsync();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter one of the following: English, Hindi, French, Greek.");
                }
            }
        }
    }
}
