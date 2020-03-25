using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleClient3
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			while (true)
			{
				Console.Write("> Input: ");
				string message = Console.ReadLine();
				byte[] buff = Encoding.UTF8.GetBytes(message);
				serverSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, serverEndPoint);

				buff = new byte[1024];
				int bytes = serverSocket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
				message = Encoding.UTF8.GetString(buff, 0, bytes);
				Console.WriteLine("Server: " + message);
			}
		}
	}
}
