using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.InputEncoding = Encoding.UTF8;
			BaiTap1();
			Console.ReadKey();
		}

		static void BaiTap1()
		{
			string message;
			byte[] buff;

			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			serverSocket.Bind(serverEndPoint);
			serverSocket.Listen(10);

			Socket clientSocket = serverSocket.Accept();

			EndPoint clientEndPoint = clientSocket.RemoteEndPoint;

			Console.WriteLine(clientEndPoint.ToString());

			while (true)
			{
				buff = new byte[1024];
				int bytes = clientSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
				message = Encoding.UTF8.GetString(buff, 0, bytes);
				Console.WriteLine("Client: " + message);
				Console.Write("You: ");
				message = Console.ReadLine();
				buff = Encoding.UTF8.GetBytes(message);
				clientSocket.Send(buff, 0, buff.Length, SocketFlags.None);
			}
		}
	}
}
