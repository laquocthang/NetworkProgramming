using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleClient4
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint remote = new IPEndPoint(IPAddress.Loopback, 5000); //Different from previous project: EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			serverSocket.Connect(remote);
			serverSocket.SendTo(Encoding.UTF8.GetBytes("Hello server"), remote);
			for (int i = 1; i <= 5; i++)
			{
				byte[] buff = Encoding.UTF8.GetBytes("Thong diep thu " + i);
				serverSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, remote);
			}
			Console.ReadKey();
		}
	}
}
