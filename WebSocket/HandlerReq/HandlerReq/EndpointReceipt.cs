using HandlerReq;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HandlerReq
{
    public class EndpointReceipt : IApiFunc
    {
        private readonly WebSocket _webSocket;
        private bool _transactionComplete;

        public EndpointReceipt(WebSocket webSocket)
        {
            _webSocket = webSocket;
            _transactionComplete = false;
        }

        public void SendResponse()
        {
            string confirm = null;

            while (true)
            {
                Console.WriteLine("Do you want to generate a receipt? (yes):");
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
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();

                    File.AppendAllText("numbers.txt", $"Receipt Number: {receiptNumber}, Cage Operator: {operatorName}, Time: {currentTime}{Environment.NewLine}");

                    Console.WriteLine("Generating receipt for the company...");
                    Console.WriteLine($"Receipt number: {receiptNumber}");

                    string thankYouMessage = "Thank you for the transaction!";
                    buffer = Encoding.UTF8.GetBytes(thankYouMessage);
                    _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None).Wait();

                    Console.WriteLine("Thank you for the transaction!");

                    _transactionComplete = true;
                    break; 
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' to generate a receipt.");
                }
            }
        }
    }
}
