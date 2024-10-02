using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebTrasportProtocol;

namespace WebTransportProtocol
{
    public class EndpointConfirmAmount : IApiFunc
    {
        public async Task SendResponseAsync(Stream transportStream, JsonElement root)
        {
            string confirm = "";

            while (confirm != "yes" && confirm != "no")
            {
                Console.WriteLine("Do you want to confirm the amount? (yes/no):");
                confirm = Console.ReadLine()?.Trim().ToLower();

                if (confirm == "yes")
                {
                    Console.WriteLine("Confirmed Payment..!");
                    string message = $"CONFIRMAMOUNT";
                    var responseBytes = Encoding.UTF8.GetBytes(message);
                    await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    await transportStream.FlushAsync();
                }
                else if (confirm == "no")
                {
                    Console.WriteLine("Payment not confirmed.");
                    string msg = "No payment confirmation from the server";
                    string noMsg = $"NOPAYMENT:{msg}";
                    var responseBytes = Encoding.UTF8.GetBytes(noMsg);
                    await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    await transportStream.FlushAsync();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no' only.");
                }
            }
        }
    }
}
