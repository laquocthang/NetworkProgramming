using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleClient
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			while (true)
			{
				byte[] buff = Encoding.UTF8.GetBytes("Hello server!");
				server.SendTo(buff, buff.Length, SocketFlags.None, serverEndPoint);
				Thread.Sleep(500);
			}
			Console.ReadKey();
		}
	}
}
