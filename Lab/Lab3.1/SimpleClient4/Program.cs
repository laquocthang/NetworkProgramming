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
			while (true)
			{
				Console.Write("> Input: ");
				string message = Console.ReadLine();
				byte[] buff = Encoding.UTF8.GetBytes(message);
				serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);

				buff = new byte[1024];
				int bytes = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
				message = Encoding.UTF8.GetString(buff, 0, bytes);
				Console.WriteLine("Server: " + message);
			}
		}
	}
}
