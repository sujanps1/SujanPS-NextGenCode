using System.Text;
using System.Text.Json;
using WebTrasportProtocol;

namespace WebTransportProtocol
{
    public class EndpointCard : IApiFunc
    {
        public async Task SendResponseAsync(Stream transportStream, JsonElement root)
        {
            string cardHolderName = "Anil";
            string bankName = "HDFC";
            string cardType = "Visa";
            string cardNumber = "1234-5678-9876";
            string transactionMode = "Online";
            string transactionType = "Purchase";

            Console.WriteLine("Card Information:");
            Console.WriteLine($"Card Holder Name: {cardHolderName}");
            Console.WriteLine($"Bank Name: {bankName}");
            Console.WriteLine($"Card Type: {cardType}");
            Console.WriteLine($"Card Number: {cardNumber}");
            Console.WriteLine($"Transaction Mode: {transactionMode}");
            Console.WriteLine($"Transaction Type: {transactionType}");

            string decision = "";

            while (decision != "yes" && decision != "no")
            {
                Console.WriteLine("Do you want to send the card details? (yes/no):");
                decision = Console.ReadLine()?.Trim().ToLower();

                if (decision == "yes")
                {
                    string dataToSend = $"CARD:{cardHolderName},{bankName},{cardType},{cardNumber},{transactionMode},{transactionType}";
                    byte[] buffer = Encoding.UTF8.GetBytes(dataToSend);
                    await transportStream.WriteAsync(buffer, 0, buffer.Length);
                    await transportStream.FlushAsync();
                }
                else if (decision == "no")
                {
                    string msg = "No Card Details From server";
                    string noMsg = $"NOCARD:{msg}";
                    byte[] buffer = Encoding.UTF8.GetBytes(noMsg);
                    await transportStream.WriteAsync(buffer, 0, buffer.Length);
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
