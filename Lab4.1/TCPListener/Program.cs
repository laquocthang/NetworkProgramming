using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPListener
{
	class Program
	{
		static void Main(string[] args)
		{
			RunWithOnlyClient();
			Console.ReadKey();
		}

		private static void RunWithOnlyClient()
		{
			// Listens for connections from TCP network clients
			TcpListener server = null;
			string data = string.Empty;
			byte[] bytes;

			try
			{
				int port = 5000;    // Set the TCPListener on port 5000
				IPAddress localAddr = IPAddress.Parse("127.0.0.1");

				server = new TcpListener(localAddr, port);
				server.Start(); // Start listening for client requests

				bytes = new byte[1024]; // Buffer for reading data

				Console.WriteLine("Waiting for a connection...");
				TcpClient client = server.AcceptTcpClient(); // Perform a blocking call to accept requests
				Console.WriteLine("Connected successfully");
				Console.WriteLine("Found client at {0}", client.Client.RemoteEndPoint.ToString());

				NetworkStream stream = client.GetStream(); // Get a stream object for reading and writing

				while (true)
				{
					data = null;

					int i;

					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) // Loop to receive all the data sent by the client
					{
						data = Encoding.UTF8.GetString(bytes, 0, i); // Translate data bytes to a UTF8 string
						if (data.Equals("exit all", StringComparison.InvariantCultureIgnoreCase))
							break;
						Console.WriteLine("Client: " + data);

						Console.Write("> Input: ");
						data = Console.ReadLine();
						byte[] msg = Encoding.UTF8.GetBytes(data);

						stream.Write(msg, 0, msg.Length);
					}
				}
				client.Close();
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: " + e);
			}
			finally
			{
				server.Stop(); // Stop listening for new clients
			}
		}

		private static void RunWithMultipleClients()
		{
			// Listens for connections from TCP network clients
			TcpListener server = null;
			try
			{
				int port = 5000;    // Set the TCPListener on port 5000
				IPAddress localAddr = IPAddress.Parse("127.0.0.1");

				server = new TcpListener(localAddr, port);
				server.Start(); // Start listening for client requests

				byte[] bytes = new byte[1024]; // Buffer for reading data
				string data;

				while (true)
				{
					Console.WriteLine("Waiting for a connection...");

					TcpClient client = server.AcceptTcpClient(); // Perform a blocking call to accept requests
					Console.WriteLine("Connected successfully");

					data = null;
					NetworkStream stream = client.GetStream(); // Get a stream object for reading and writing

					int i;

					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) //Loop to receive all the data sent by the client
					{
						data = Encoding.UTF8.GetString(bytes, 0, i); //Translate data bytes to a UTF8 string
						Console.WriteLine("Client: " + data);

						Console.Write("> Input: ");
						data = Console.ReadLine();
						byte[] msg = Encoding.UTF8.GetBytes(data);

						stream.Write(msg, 0, msg.Length);
					}
					client.Close();
				}
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: " + e);
			}
			finally
			{
				server.Stop(); // Stop listening for new clients
			}
		}
	}
}
