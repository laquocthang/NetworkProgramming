using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPLibrary
{
	public class NewUDPClient
	{
		private byte[] data;

		/// <summary>
		/// Send and wait for a response from client
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private int SendReceiveData(byte[] message, Socket socket, EndPoint remote)
		{
			int receive;
			int retry = 0;

			while (true)
			{
				Console.WriteLine("Truyen lai lan thu #{0}", retry);
				try
				{
					socket.SendTo(message, message.Length, SocketFlags.None, remote);
					data = new byte[1024];
					receive = socket.ReceiveFrom(data, ref remote);
				}
				catch (SocketException)
				{
					receive = 0;
				}
				if (receive > 0)
				{
					return receive;
				}
				else
				{
					retry++;
					if (retry > 4)
						return 0;
				}
			}
		}

		public void Run()
		{
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			// Default timeout
			int socketOption = (int)serverSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			Console.WriteLine("The default timeout: " + socketOption);

			// New timeout
			serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
			socketOption = (int)serverSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			Console.WriteLine("The new timeout: " + socketOption);

			string message = "Hello Server";
			byte[] buff = Encoding.UTF8.GetBytes(message);
			int bytes = SendReceiveData(buff, serverSocket, serverEndPoint); // Send and wait for receiving a message

			if (bytes > 0)
			{
				message = Encoding.UTF8.GetString(buff, 0, bytes);
				Console.WriteLine("Server: " + message);
			}
			else
			{
				Console.WriteLine("Can't connect to remote target");
				return;
			}

			while (true)
			{
				Console.Write("Input: ");
				message = Console.ReadLine();
				if (message.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
					break;
				buff = Encoding.UTF8.GetBytes(message);
				bytes = SendReceiveData(buff, serverSocket, serverEndPoint);
				if (bytes > 0)
				{
					message = Encoding.UTF8.GetString(data, 0, bytes);
					Console.WriteLine("Server: " + message);
				}
				else
				{
					Console.WriteLine("Can't connect to remote target");
				}
			}
			Console.WriteLine("Client is exiting...");
			serverSocket.Close();
		}

	}
}
