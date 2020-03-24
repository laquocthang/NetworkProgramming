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
			IPEndPoint server = new IPEndPoint(IPAddress.Loopback, 5000);
			//Create a socket
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Console.WriteLine("Connecting to server ... ");
			//Connect to server
			socket.Connect(server);
			//The progress of receiving data
			if (socket.Connected)
			{
				Console.WriteLine("Connected Successfully");
				byte[] buff = new byte[32];
				int bytes = socket.Receive(buff, 0, buff.Length, SocketFlags.None);
				string message = Encoding.ASCII.GetString(buff, 0, buff.Length);
				Console.WriteLine(message);
			}

			Console.ReadKey();
		}
	}
}
