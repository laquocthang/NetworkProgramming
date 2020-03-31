using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDPLibrary;

namespace UDPClient
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

			int socketOption = (int)serverSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			Console.WriteLine("The default timeout: " + socketOption);

			serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
			socketOption = (int)serverSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			Console.WriteLine("The new timeout: " + socketOption);

			string message = "Hello Server";
			byte[] buff = Encoding.UTF8.GetBytes(message);
			int bytes = Send.SendData(ref buff, serverSocket, serverEndPoint);
			if (bytes > 0)
			{
				message = Encoding.UTF8.GetString(buff, 0, bytes);
				Console.WriteLine(message);
			}
			else
			{
				Console.WriteLine("Can't connect to remote target");
				return;
			}

			while (true)
			{
				Console.Write("Input: ");
				message = Console.ReadLine();
				if (message.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
					break;
				buff = Encoding.UTF8.GetBytes(message);
				bytes = Send.SendData(ref buff, serverSocket, serverEndPoint);
				if (bytes > 0)
				{
					message = Encoding.UTF8.GetString(buff, 0, bytes);
					Console.WriteLine("Server: " + message);
				}
				else
				{
					Console.WriteLine("Can't connect to remote target");
				}
			}
			Console.WriteLine("Client is exiting...");
			serverSocket.Close();
		}
	}
}
