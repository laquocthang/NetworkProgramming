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
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			serverSocket.Bind(serverEndPoint);
			serverSocket.Listen(10);

			Socket clientSocket = serverSocket.Accept();

			EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
			Console.WriteLine(clientEndPoint.ToString());

			while (true)
			{
				//Receive the message from client
				byte[] buff = new byte[1024];
				int bytes = clientSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
				string message = Encoding.ASCII.GetString(buff, 0, bytes);
				Console.WriteLine("Received Message: " + message);
				//Send this message to client
				clientSocket.Send(buff, 0, bytes, SocketFlags.None);
			}
		}
	}
}
