using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleClient
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

			byte[] buff = Encoding.UTF8.GetBytes("Hello server!");
			Console.WriteLine("Sending a message to server...");
			server.SendTo(buff, buff.Length, SocketFlags.None, serverEndPoint);
			Console.WriteLine("Sended message successfully");
			Console.ReadKey();
		}
	}
}
