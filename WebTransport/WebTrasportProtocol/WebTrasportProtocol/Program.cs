using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using WebTransportProtocol;
using WebTrasportProtocol;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http3;
        listenOptions.UseHttps();
    });
});


var app = builder.Build();

app.UseRouting();

app.Map("/webtransport", async context =>
{
    var feature = context.Features.GetRequiredFeature<IHttpWebTransportFeature>();

    if (!feature.IsWebTransportRequest)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("Not a WebTransport request.");
        return;
    }

    var session = await feature.AcceptAsync(CancellationToken.None);
    var stream = await session.AcceptStreamAsync(CancellationToken.None);

    if (stream != null)
    {
        Console.WriteLine("Connected");

        var inputPipe = stream.Transport.Input;

        while (true)
        {
            var direction = stream.Features.GetRequiredFeature<IStreamDirectionFeature>();
            if (!direction.CanRead || !direction.CanWrite)
            {
                break;
            }

            var buffer = new byte[1024];
            var length = await inputPipe.AsStream().ReadAsync(buffer, 0, buffer.Length, CancellationToken.None);
            if (length == 0)
            {
                break;
            }

            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine("Received message from client: " + receivedMessage);

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(receivedMessage))
                {
                    JsonElement root = doc.RootElement;

                    if (root.TryGetProperty("type", out JsonElement typeElement))
                    {
                        string type = typeElement.GetString();
                        Console.WriteLine($"Type: {type}");
                        string user = root.TryGetProperty("user", out JsonElement userElement) ? userElement.GetString() : "Unknown";
                        string password = root.TryGetProperty("password", out JsonElement passwordElement) ? passwordElement.GetString() : "Unknown";

                        Console.WriteLine($"User: {user}");
                        Console.WriteLine($"Password: {password}");
                        IApiFunc endpoint = type switch
                        {
                            "Language" => new EndpointLanguage(),
                            "SSN" => new EndpointSSN(),
                            "Card" => new EndpointCard(),
                            "Amount" => new EndpointAmount(),
                            "ConfirmAmount" => new EndpointConfirmAmount(),
                            "Signature" => new EndpointSignature(),
                            "Receipt" => new EndpointReceipt(),
                            _ => null
                        };

                        if (endpoint != null)
                        {
                            await endpoint.SendResponseAsync(stream.Transport.Output.AsStream(), root);
                        }
                        var Login = new EndpointLogin();

                        if (type == "Login")
                        {
                            Login.SendResponse(user, password, stream.Transport.Output.AsStream(), root);
                        }
                        //else if (type == "LogOut")
                        //{
                        //    await logout.SendResponse(user, password, socket);
                        //}
                        else if (type == "SendSearchinfo")
                        {
                            if (root.TryGetProperty("patroninformation", out JsonElement patronElement))
                            {
                                var patronInfo = patronElement.GetRawText();
                                //below commented line is for Cash Advance service API

                                //var patroninformation = new Patroninfo();

                                //this is for CAPS API
                                var patroninformation = new Patroninfocaps();

                                patroninformation.Patroninformation(patronInfo, stream.Transport.Output.AsStream(), root);
                            }
                            else
                            {

                                Console.WriteLine("Patron information not found.");
                            }
                        }

                        else if (type == "Patroninformation")
                        {
                            if (root.TryGetProperty("patroninformation", out JsonElement patronElement))
                            {
                                var patronInfo = patronElement.GetRawText();
                                var patronentity = new PatronAddiCaps();
                                Console.WriteLine(patronInfo);
                                patronentity.Patroninformation(patronInfo, stream.Transport.Output.AsStream(), root);
                                Console.WriteLine($"SuccessFully added patron data");
                            }
                        }
                        else
                        {
                            //Console.WriteLine($"No endpoint found for type: {type}");
                        }
                    }
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Error parsing JSON: {e.Message}");
            }
        }

        await stream.DisposeAsync();
    }
});

await app.RunAsync();
