using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
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

		private static void BaiTap1()
		{
			string message;
			byte[] buff;

			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Console.WriteLine("Connecting to server...");
			try
			{
				Console.WriteLine("Connected successfully");
				serverSocket.Connect(serverEndPoint);
			}
			catch (SocketException)
			{
				Console.WriteLine("Can't connect to server. Press any key to exit");
				return;
			}
			while (true)
			{
				Console.Write("You: ");
				message = Console.ReadLine();
				buff = Encoding.UTF8.GetBytes(message);
				serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);
				buff = new byte[1024];
				int bytes = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
				message = Encoding.UTF8.GetString(buff);
				Console.WriteLine("Server: " + message);
			}
		}
	}
}
