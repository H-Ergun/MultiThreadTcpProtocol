using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sender
{
    public class ClientTcp
    {
        private TcpClient? client { get; set; }

        private StreamWriter? sWriter { get; set; }

        public void SetEndPoint(string ipAddress, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ipAddress);

            client ??= new TcpClient();

            client.Connect(iPAddress, port);
        }

        public void SendMessage(ThreadPoolModel model)
        {
            if (client != null)
            {
                string jsonMessage = JsonConvert.SerializeObject(model);

                sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);

                sWriter.WriteLine(jsonMessage);

                sWriter.Flush();

                Console.WriteLine($"Gönderilen mesaj = {jsonMessage}");

                return;

            }

            throw new ArgumentNullException("Client cannot be null. First try running the Connect method.");
        }
    }
}
