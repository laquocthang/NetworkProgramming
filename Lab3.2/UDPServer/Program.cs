using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDPServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Run();
			Console.ReadKey();
		}

		static void Run()
		{
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			serverSocket.Bind(serverEndPoint);

			byte[] buff = new byte[1024];
			string message;

			Console.WriteLine("Waiting for client...");
			EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			int bytes = serverSocket.ReceiveFrom(buff, ref remote);

			Console.WriteLine("Found client: " + remote.ToString());
			Console.WriteLine("Client: " + Encoding.UTF8.GetString(buff, 0, bytes));

			while (true)
			{
				Thread.Sleep(16000);
				message = "Hello Client";
				buff = Encoding.UTF8.GetBytes(message);
				serverSocket.SendTo(buff, buff.Length, SocketFlags.None, remote); //Response a message
			}
		}
	}
}
