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
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket socketEndPoint = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

			//socket.Connect(remote);

			byte[] buff;
			string message;

			while (true)
			{
				Console.Write("\nDo you want to exit? Enter \"exit\" to close client, or \"exit all\" to close all, any key to continue: ");
				//Send the message to server
				Console.Write("\nInput : ");
				message = Console.ReadLine();
				buff = Encoding.ASCII.GetBytes(message);
				Console.WriteLine("Sending this message to server...");
				socketEndPoint.SendTo(buff, serverEndPoint);

				//Receive the message from server
				int bytes;
				buff = new byte[10];
				try
				{
					bytes = socketEndPoint.ReceiveFrom(buff, ref remote);
					message = Encoding.ASCII.GetString(buff, 0, bytes);
					Console.WriteLine("Received the message from server: " + message);
				}
				catch (SocketException)
				{
					Console.WriteLine("The data is missing, please retry!");
				}

				string command = Console.ReadLine();
				if (command.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
					break;
				else if (command.Equals("exit all", StringComparison.InvariantCultureIgnoreCase))
				{
					buff = Encoding.ASCII.GetBytes(command);
					socketEndPoint.SendTo(buff, serverEndPoint);
					break;
				}
			}
			socketEndPoint.Close();
		}
	}
}
