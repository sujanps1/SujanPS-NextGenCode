using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebTrasportProtocol;

namespace WebTransportProtocol
{
    public class EndpointSSN : IApiFunc
    {
        public async Task SendResponseAsync(Stream transportStream, JsonElement root)
        {
            string decision = "";

            while (decision != "yes" && decision != "no")
            {
                Console.WriteLine("Do you want to send the SSN data? (yes/no):");
                decision = Console.ReadLine()?.Trim().ToLower();

                if (decision == "yes")
                {
                    Console.WriteLine("Please enter a 10-digit number:");
                    string SSN = Console.ReadLine();

                    Console.WriteLine($"SSN Number is : {SSN}");

                    string message = $"SSN:{SSN}";
                    var responseBytes = Encoding.UTF8.GetBytes(message);
                    await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    await transportStream.FlushAsync();
                }
                else if (decision == "no")
                {
                    string msg = "No SSN data from the server";
                    string noMsg = $"NOSSN:{msg}";
                    var responseBytes = Encoding.UTF8.GetBytes(noMsg);
                    await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    await transportStream.FlushAsync();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 'yes' or 'no' only.");
                }
            }
        }
    }
}
