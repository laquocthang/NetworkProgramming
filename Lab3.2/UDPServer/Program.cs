using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

			serverSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, remote); // Send a duplicate to client to response

			while (true)
			{
				buff = new byte[1024];
				bytes = serverSocket.ReceiveFrom(buff, ref remote);
				message = Encoding.UTF8.GetString(buff, 0, bytes);
				Console.WriteLine("Client: " + message);

				serverSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, remote);

				Console.Write("Input: ");
				message = Console.ReadLine();
				buff = Encoding.UTF8.GetBytes(message);
				serverSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, remote);
			}
		}
	}
}
