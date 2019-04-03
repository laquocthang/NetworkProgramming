using System;
using System.Net;
using System.Net.Sockets;

namespace SimpleServer
{
	class Program
	{
		static void Main(string[] args)
		{
			//Create EndPoint Server and its parameters included IP and Port
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
			//Create Socket that connect to Server
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			//Try to bind a socket with specific address
			serverSocket.Bind(serverEndPoint);
			//Listen to connection to Socket
			serverSocket.Listen(10);
			//Accept the connection to Socket
			Socket clientSocket = serverSocket.Accept();
			//Display client's information
			EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
			Console.WriteLine(clientEndPoint.ToString());

			Console.ReadKey();
		}
	}
}
