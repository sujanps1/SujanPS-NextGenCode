using System.Text.Json;

namespace WebTrasportProtocol
{
    public interface IApiFunc
    {
        Task SendResponseAsync(Stream transportStream, JsonElement root);
    }
}
