using Receiver;
using Shared;
using System.Net;

try
{
    ServerTcp serverTcp = new();

    serverTcp.SetEndPoint(IPAddress.Any, Tcp.Port);

    while (true)
    {
        serverTcp.MessagePrint();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Hata: {ex.Message}");
}

Console.ReadKey();
