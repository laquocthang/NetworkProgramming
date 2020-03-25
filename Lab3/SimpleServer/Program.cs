using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleServer
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			byte[] buff = new byte[1024];
			serverSocket.Bind(serverEndPoint);

			Console.WriteLine("Waiting for client...");
			EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			serverSocket.ReceiveFrom(buff, ref remote);
			Console.WriteLine("Client Info: " + remote.ToString());
			Console.WriteLine(Encoding.UTF8.GetString(buff).Replace("\0", ""));
			Console.ReadKey();
		}
	}
}
