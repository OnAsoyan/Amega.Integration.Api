using System.Net.WebSockets;
using System.Text;

namespace Amega.Integration.Api.Socket;

public class WebSocketManager
{
    private static readonly List<WebSocket> _sockets = [];

    public static void AddSocket(WebSocket socket)
    {
        _sockets.Add(socket);
    }

    public static async Task RemoveSocket(WebSocket socket)
    {
        _sockets.Remove(socket);
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by the WebSocketManager", CancellationToken.None);
    }

    public static async Task BroadcastMessageAsync(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        foreach (var socket in _sockets)
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
