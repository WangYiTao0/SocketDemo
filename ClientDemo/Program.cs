using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        private static Socket clientSocket = null;

        private static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //建立与远程服务器的连接
            //本机默认IP 127.0.0.1
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            clientSocket.Connect(remoteEP);
            Console.WriteLine("Connection With Server Successed");


            //收到消息
            byte[] result = new byte[1024];
            int length = clientSocket.Receive(result);
            Console.WriteLine("3. Receive Message from Server" + Encoding.Default.GetString(result, 0, length));

            clientSocket.Send(Encoding.Default.GetBytes("4. Send Message to Server Hello Server I am Client1"));


            while (true) { }
        }


    }
}