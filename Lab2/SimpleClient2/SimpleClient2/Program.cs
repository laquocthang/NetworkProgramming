using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleClient2
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint server = new IPEndPoint(IPAddress.Loopback, 5000);
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Console.WriteLine("Connecting to server ... ");
			try
			{
				socket.Connect(server);
			}
			catch (SocketException se)
			{
				Console.WriteLine("Can't connect to server");
				Console.ReadKey();
				return;
			}

			if (socket.Connected)
			{
				Console.WriteLine("Connected Successfully");
				int bytes;
				byte[] buff = new byte[32];
				bytes = socket.Receive(buff, 0, buff.Length, SocketFlags.None);
				string message = Encoding.ASCII.GetString(buff, 0, bytes);
				Console.WriteLine(message);
			}

			Console.ReadKey();
		}
	}
}
