using System.Text;
using System.Text.Json;

namespace WebTrasportProtocol
{
    public class EndpointAmount : IApiFunc
    {
        public async Task SendResponseAsync(Stream transportStream, JsonElement root)
        {
            string amount = null;

            while (true)
            {
                Console.WriteLine("Please enter the amount in dollars (e.g., 100 or 99.99):");

                amount = Console.ReadLine()?.Trim();

                if (decimal.TryParse(amount, out decimal parsedAmount) && parsedAmount > 0)
                {
                    Console.WriteLine($"Entered Amount: ${parsedAmount}");

                    string message = $"AMOUNT:{parsedAmount}";
                    var responseBytes = Encoding.UTF8.GetBytes(message);
                    await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    await transportStream.FlushAsync();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a valid dollar amount (numbers only).");
                }
            }
        }

    }
}
