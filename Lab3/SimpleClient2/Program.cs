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
			EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

			byte[] buff = Encoding.UTF8.GetBytes("Hello server!");
			serverSocket.SendTo(buff, buff.Length, SocketFlags.None, serverEndPoint);

			Console.Write("Input: ");
			string message = Console.ReadLine();
			if (message.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
			{
				serverSocket.Close();
				Environment.Exit(-1);
			}
			else if (message.Equals("exit all", StringComparison.InvariantCultureIgnoreCase))
			{
				buff = Encoding.UTF8.GetBytes(message);
				serverSocket.SendTo(buff, buff.Length, SocketFlags.None, serverEndPoint);
				serverSocket.Close();
				Environment.Exit(-1);
			}
			buff = Encoding.UTF8.GetBytes(message);
			serverSocket.SendTo(buff, buff.Length, SocketFlags.None, serverEndPoint);
			Console.ReadKey();
		}
	}
}
