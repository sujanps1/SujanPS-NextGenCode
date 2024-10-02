using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace HandlerReq
{
    public class EndpointCard : IApiFunc
    {
        private readonly WebSocket _webSocket;

        private readonly string cardHolderName = "Anil";
        private readonly string bankName = "HDFC";
        private readonly string cardType = "Visa";
        private readonly string cardNumber = "1234-5678-9876";
        private readonly string transactionMode = "Online";
        private readonly string transactionType = "Purchase";

        public EndpointCard(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void SendResponse()
        {
            Console.WriteLine("Card Information : ");
            Console.WriteLine($"Card Holder Name: {cardHolderName}");
            Console.WriteLine($"Bank Name: {bankName}");
            Console.WriteLine($"Card Type: {cardType}");
            Console.WriteLine($"Card Number: {cardNumber}");
            Console.WriteLine($"Transaction Mode: {transactionMode}");
            Console.WriteLine($"Transaction Type: {transactionType}");

            string decision = null;

            while (decision != "yes" && decision != "no")
            {
                Console.WriteLine("Do you want to send this data to the client? (yes/no):");
                decision = Console.ReadLine()?.Trim().ToLower();

                if (decision == "yes")
                {
                    string dataToSend = $"CARD:{cardHolderName},{bankName},{cardType},{cardNumber},{transactionMode},{transactionType}";
                    byte[] buffer = Encoding.UTF8.GetBytes(dataToSend);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                    break;
                }
                else if (decision == "no")
                {
                    string msg = "No Card Details From server";
                    string Nomsg = $"NOCARD:{msg}";
                    byte[] buffer = Encoding.UTF8.GetBytes(Nomsg);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no' only.");
                }
            }
        }
    }
}
