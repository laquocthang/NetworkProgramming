using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleServer2
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
			int bytes = serverSocket.ReceiveFrom(buff, ref remote);
			Console.WriteLine("Client Info: " + remote.ToString());
			Console.WriteLine("Client: " + Encoding.UTF8.GetString(buff, 0, bytes));
			while (true)
			{
				Console.Write("> Input: ");
				string message = Console.ReadLine();
				buff = Encoding.UTF8.GetBytes(message);
				serverSocket.SendTo(buff, 0, buff.Length, SocketFlags.None, remote);

				buff = new byte[1024];
				bytes = serverSocket.ReceiveFrom(buff, ref remote);
				message = Encoding.UTF8.GetString(buff, 0, bytes);
				Console.WriteLine("Client: " + message);
			}
		}
	}
}
