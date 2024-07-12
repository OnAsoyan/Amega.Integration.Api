using System.Net.WebSockets;

namespace Amega.Integration.Api.Socket;

public static class WebSocketHandler
{
    public static async Task HandleAsync(WebSocket webSocket)
    {
        WebSocketManager.AddSocket(webSocket);
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await WebSocketManager.RemoveSocket(webSocket);
    }
}
