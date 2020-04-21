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
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			serverSocket.Connect(serverEndPoint);
			serverSocket.Send(Encoding.UTF8.GetBytes("Hello server"));

			for (int i = 1; i <= 5; i++)
			{
				byte[] buff = Encoding.UTF8.GetBytes("Thong diep thu " + i);
				serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);
			}
			Console.ReadKey();
		}
	}
}
