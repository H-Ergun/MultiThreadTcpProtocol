using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Receiver
{

    public class ServerTcp
    {
        private TcpListener? listener { get; set; }     

        public void SetEndPoint(IPAddress IPAddress, int port)
        {
            IPAddress iPAddress = IPAddress;

            listener ??= new TcpListener(iPAddress, port);

            listener.Start();
        }

        public void MessagePrint()
        {   
            TcpClient client = listener?.AcceptTcpClient() ?? throw new ArgumentNullException("listener cannot be null");

            Thread t = new(new ParameterizedThreadStart(HandleClient));

            t.Start(client);
        }

        static void HandleClient(object? obj)
        {

            TcpClient client = (TcpClient?)obj
                               ?? throw new ArgumentNullException("Client cannot be null");

            // Ağ akışlarını al

            StreamReader sReader = new(client.GetStream(), Encoding.UTF8);

            //NetworkStream stream = client.GetStream();

            while (true)
            {
                try
                {
                    var receivedData = sReader.ReadLine();

                    var cleanData = receivedData?.ToCharArray().Where(x => x != 65279).ToArray();

                    string res = new(cleanData);

                    Console.WriteLine($"Alınan Veri: {res}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }







    }

}
