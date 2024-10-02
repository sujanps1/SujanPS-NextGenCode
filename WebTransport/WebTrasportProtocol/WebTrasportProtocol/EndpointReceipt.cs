using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebTrasportProtocol;

namespace WebTransportProtocol
{
    public class EndpointReceipt : IApiFunc
    {
        public async Task SendResponseAsync(Stream transportStream, JsonElement root)
        {
            string confirm;

            while (true)
            {
                Console.WriteLine("Do you want to receive a receipt? (yes):");
                confirm = Console.ReadLine()?.Trim().ToLower();

                if (confirm == "yes")
                {
                    var random = new Random();
                    int receiptNumber = random.Next(1000, 9999);
                    DateTime currentTime = DateTime.Now;

                    Console.WriteLine($"Receipt number: {receiptNumber}");
                    Console.WriteLine("Enter the cage operator's name:");
                    string operatorName = Console.ReadLine();

                    Console.WriteLine($"Receipt number: {receiptNumber}, Cage Operator: {operatorName}, Time: {currentTime}");

                    string message = $"FINAL: Receipt number {receiptNumber}, Cage Operator: {operatorName}, Time: {currentTime}";
                    var responseBytes = Encoding.UTF8.GetBytes(message);
                    await transportStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    await transportStream.FlushAsync();

                    File.AppendAllText("number.txt", $"Receipt Number: {receiptNumber}, Cage Operator: {operatorName}, Time: {currentTime}{Environment.NewLine}");

                    Console.WriteLine("Generating receipt for the company...");
                    Console.WriteLine($"Receipt number: {receiptNumber}");

                    string thankYouMessage = "Thank you for the transaction!";
                    var responseBytess = Encoding.UTF8.GetBytes(thankYouMessage);
                    await transportStream.WriteAsync(responseBytess, 0, responseBytess.Length);
                    await transportStream.FlushAsync();

                    Console.WriteLine("Thank you for the transaction!");
                    break; 
                }
                else
                {
                    Console.WriteLine("Invalid input. Please type 'yes' to generate a receipt.");
                }
            }
        }
    }
}
