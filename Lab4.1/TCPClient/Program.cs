using System;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Connect("127.0.0.1", "La Quoc Thang");
			Console.ReadKey();
		}

		static void Connect(string server, string message)
		{
			try
			{
				int port = 5000;
				TcpClient client = new TcpClient(server, port); // Create a TcpClient

				byte[] data = Encoding.UTF8.GetBytes(message); // Translate the passed message into UTF8 and store it as a byte array

				NetworkStream stream = client.GetStream(); // Get a client stream for reading and writing

				stream.Write(data, 0, data.Length); // Send the message to the connected TcpServer

				data = new byte[1024]; // String to store the response UTF8 representation.
				string response = String.Empty;
				int bytes = stream.Read(data, 0, data.Length); // Read the first batch of the TcpServer response bytes.
				response = Encoding.UTF8.GetString(data, 0, bytes);
				Console.WriteLine("Server: " + response);

				stream.Close();
				client.Close();
			}
			catch (ArgumentNullException e)
			{
				Console.WriteLine("ArgumentNullException: {0}", e);
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}
		}
	}
}
