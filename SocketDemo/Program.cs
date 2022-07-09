using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketDemo
{
    class Program
    {
        private static Socket serverSocket = null;
        /// <summary>
        /// 服务器常做的几件事
        /// 接受请求
        /// 发送数据
        /// 接受数据
        /// 断开连接
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Socket 应用层 和 运输层之间
            //SocketType.Stream 指定类型
            // ProtocolType.Tcp 指定协议
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //绑定到应用上
            //服务器要监听所有的IP
            //端口范围 0 - 65535
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9999);
            serverSocket.Bind(endPoint);
            //连接队列的最大长度
            serverSocket.Listen(10);
            //开启线程 接受客户端连接
            Console.WriteLine("1. Server Start Receive Message");
            Thread thread = new Thread(ListenClientConnect);
        
            thread.Start();

            while (true) { }

        }

        /// <summary>
        /// 监听客户端数据
        /// </summary>
        private static void ListenClientConnect()
        {
            //返回客户端sorket 对象
            var clientSocket = serverSocket.Accept();
            Console.WriteLine("2. Client Connection Successed : " + clientSocket.AddressFamily.ToString());
            clientSocket.Send(Encoding.Default.GetBytes("Server tell Client Connection Successed！"));


            Thread thread = new Thread(ReceiveClientMessage);
            thread.Start(clientSocket);
        }

        /// <summary>
        /// 接受来自客户端的消息
        /// </summary>
        private static void ReceiveClientMessage(object? clientSocket)
        {
            Socket socket = clientSocket as Socket;
            byte[] buffer = new byte[1024];
            //接受到数据的长度
            int length = socket.Receive(buffer);
            //显示出来
            Console.WriteLine("5. Get Message from Client " + Encoding.Default.GetString(buffer,0,length));
        }
    }
}