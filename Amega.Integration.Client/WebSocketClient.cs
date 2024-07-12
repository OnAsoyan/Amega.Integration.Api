using System.Net.WebSockets;
using System.Text;

public class WebSocketClient(string serverUrl) : IDisposable
{
    private readonly Uri _serverUri = new(serverUrl);
    private ClientWebSocket _webSocket = new();

    public async Task ConnectAsync()
    {
        try
        {
            await _webSocket.ConnectAsync(_serverUri, CancellationToken.None);
            Console.WriteLine("Connected to WebSocket server.");
            await ReceiveMessages();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to WebSocket server: {ex.Message}");
        }
    }

    public async Task SendMessageAsync(string message)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<byte>(messageBytes);
            await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    private async Task ReceiveMessages()
    {
        var buffer = new byte[1024 * 4];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received message: {message}");
            }
        }
    }

    public async Task CloseAsync()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            _webSocket.Dispose();
            Console.WriteLine("WebSocket connection closed.");
        }
    }

    public void Dispose()
    {
        _webSocket?.Dispose();
    }
}
