using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpEchoClient
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 3)
			{
				Console.WriteLine("Missing parameters!");
				return;
			}

			int port;
			string message;
			try
			{
				port = int.Parse(args[1]);
				message = args[2];
			}
			catch (Exception)
			{
				Console.WriteLine("Invalid parameters!");
				return;
			}

			TcpClient client;
			try
			{
				client = new TcpClient(args[0], port);
			}
			catch (SocketException e)
			{
				Console.WriteLine("Can't connect to server");
				Console.WriteLine("Error: " + e.Message);
				return;
			}

			NetworkStream stream = client.GetStream();
			byte[] buff = Encoding.UTF8.GetBytes(message);
			stream.Write(buff, 0, buff.Length);
			
			stream.Close();
			client.Close();
		}
	}
}
