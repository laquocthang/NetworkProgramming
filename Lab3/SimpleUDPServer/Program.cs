using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleUDPServer
{
	class Program
	{
		static void Main(string[] args)
		{
			//Create EndPoint Server with IP and Port
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			//Create Socket Server
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			//Connect socket to local IPEndPoint
			serverSocket.Bind(serverEndPoint);

			System.Console.WriteLine("Connecting to any clients...");
			IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
			EndPoint remote = (EndPoint)sender;

			byte[] buff = new byte[1024];
			string message;
			int bytes;

			while (true)
			{
				//Receive the message from client
				bytes = serverSocket.ReceiveFrom(buff, ref remote);
				message = Encoding.ASCII.GetString(buff, 0, bytes);
				Console.WriteLine("Received message from client: " + message);

				//Send this message to client
				serverSocket.SendTo(buff, bytes, SocketFlags.None, remote);
			}

			/*
			for (int i = 1; i <= 5; i++)
			{
				bytes = socket.ReceiveFrom(buff, 0, buff.Length, SocketFlags.None, ref remote);
				message = Encoding.ASCII.GetString(buff, 0, bytes);
				Console.WriteLine(message);
			}
			*/
			Console.ReadKey();
		}
	}
}
