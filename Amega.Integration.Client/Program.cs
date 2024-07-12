using System.Net.WebSockets;

namespace Amega.Integration.Client;

internal class Program
{
    static void Main(string[] args)
    {
        using var client = new WebSocketClient("wss://localhost:7179");
        client.ConnectAsync().GetAwaiter().GetResult();

        Console.WriteLine("Press any key to close the connection...");
        Console.ReadKey();

        client.CloseAsync().GetAwaiter().GetResult();
    }
}
