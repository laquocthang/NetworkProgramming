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
			IPEndPoint server = new IPEndPoint(IPAddress.Any, 5000);
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			socket.Bind(server);
			socket.Listen(10);

			Socket clientSocket = socket.Accept();

			EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
			Console.WriteLine(clientEndPoint.ToString());

			//Send message to client
			byte[] buff;
			string message = "Hello Client";
			buff = Encoding.ASCII.GetBytes(message);
			clientSocket.Send(buff, 0, buff.Length, SocketFlags.None);

			Console.ReadKey();
		}
	}
}
