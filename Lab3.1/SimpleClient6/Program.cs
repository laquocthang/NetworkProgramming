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
			Cau6_2();
		}

		static void Cau6_1()
		{
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint remote = new IPEndPoint(IPAddress.Loopback, 5000); //Different from previous project: EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			serverSocket.Connect(remote);
			serverSocket.Send(Encoding.UTF8.GetBytes("Hello server"));
			string message;
			byte[] buff;
			int bytes;
			while (true)
			{
				Console.Write("Input: ");
				message = Console.ReadLine();
				buff = Encoding.ASCII.GetBytes(message);
				serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);

				buff = new byte[10]; // Error when the message is larger than 10 characters
				bytes = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
				message = Encoding.ASCII.GetString(buff, 0, bytes);
				Console.WriteLine("Server: " + message);
			}
		}

		static void Cau6_2()
		{
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			EndPoint remote = new IPEndPoint(IPAddress.Loopback, 5000);
			serverSocket.Connect(remote);
			serverSocket.Send(Encoding.UTF8.GetBytes("Hello server"));

			string message;
			byte[] buff;
			int i = 10;
			int bytes;
			while (true)
			{
				Console.Write("Input: ");
				message = Console.ReadLine();
				if (message.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
					break;
				buff = Encoding.UTF8.GetBytes(message);
				serverSocket.Send(buff, buff.Length, SocketFlags.None);
				buff = new byte[i];
				try
				{
					bytes = serverSocket.Receive(buff);
					message = Encoding.UTF8.GetString(buff, 0, bytes);
					Console.WriteLine("Server: " + message);
				}
				catch (SocketException)
				{
					Console.WriteLine("Canh bao: du lieu bi mat, hay thu lai");
					i += 10;
				}
			}
		}
	}
}
