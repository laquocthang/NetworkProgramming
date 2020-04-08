using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClient2
{
	// Server side
	class Program
	{
		static void Main(string[] args)
		{
			Connect(11000);
			Console.ReadKey();
		}

		static void Connect(int port)
		{
			UdpClient udpClient = new UdpClient(port);
			IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			Console.WriteLine("Waiting for a connection...");

			while (true)
			{
				byte[] bytes = udpClient.Receive(ref remote);
				// Console.WriteLine("Connected successfully");
				// Console.WriteLine("Host info: " + remote);
				string message = Encoding.UTF8.GetString(bytes);
				if (message.Equals("exit all", StringComparison.InvariantCultureIgnoreCase))
					break;
				Console.WriteLine("Client: " + message);

				Console.Write("> Input: ");
				message = Console.ReadLine();
				bytes = Encoding.UTF8.GetBytes(message);
				udpClient.Send(bytes, bytes.Length, remote);
			}
			udpClient.Close();
		}

	}
}
