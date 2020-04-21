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
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Console.WriteLine("Connecting to server ... ");
			try
			{
				Console.WriteLine("Connected Successfully");
				serverSocket.Connect(serverEndPoint);
			}
			catch (SocketException)
			{
				Console.WriteLine("Can't connect to server");
				Console.ReadKey();
				return;
			}

			while (true)
			{
				//Send message from client to server
				Console.Write("Message: ");
				string message = Console.ReadLine();
				byte[] buff = Encoding.ASCII.GetBytes(message);
				serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);
				//Receive the message that server sent
				buff = new byte[1024];
				int bytes = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
				message = Encoding.ASCII.GetString(buff, 0, bytes);
				Console.WriteLine("Sended Message: " + message);
			}
		}
	}
}
