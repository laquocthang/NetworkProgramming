using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClient1
{
	// Client side
	class Program
	{
		static void Main(string[] args)
		{
			Connect("127.0.0.1", 11000);
			Console.ReadKey();
		}

		static void Connect(string server, int port)
		{
			UdpClient udpClient = new UdpClient();
			try
			{
				udpClient.Connect(server, port);
				string message = string.Empty;
				byte[] bytes;

				//IPEndPoint object will allow us to read datagrams sent from any source.
				IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

				while (true)
				{
					// Sends a message to the host to which you have connected.
					Console.Write("> Input: ");
					message = Console.ReadLine();
					if (message.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
						break;
					bytes = Encoding.ASCII.GetBytes(message);

					udpClient.Send(bytes, bytes.Length);

					// Blocks until a message returns on this socket from a remote host.
					bytes = udpClient.Receive(ref RemoteIpEndPoint);
					message = Encoding.ASCII.GetString(bytes);

					// Uses the IPEndPoint object to determine which of these two hosts responded.
					Console.WriteLine("Server: " + message);
					// Console.WriteLine("Host info: {0}:{1}", RemoteIpEndPoint.Address.ToString(), RemoteIpEndPoint.Port.ToString());
				}
				udpClient.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e);
			}
		}

		static void Connect_2()
		{
			UdpClient udpClient = new UdpClient(11000);
			try
			{
				udpClient.Connect("www.contoso.com", 11000);

				// Sends a message to the host to which you have connected.
				Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");

				udpClient.Send(sendBytes, sendBytes.Length);

				// Sends a message to a different host using optional hostname and port parameters.
				UdpClient udpClientB = new UdpClient();
				udpClientB.Send(sendBytes, sendBytes.Length, "AlternateHostMachineName", 11000);

				//IPEndPoint object will allow us to read datagrams sent from any source.
				IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

				// Blocks until a message returns on this socket from a remote host.
				Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
				string returnData = Encoding.ASCII.GetString(receiveBytes);

				// Uses the IPEndPoint object to determine which of these two hosts responded.
				Console.WriteLine("This is the message you received " + returnData);
				Console.WriteLine("This message was sent from " + RemoteIpEndPoint.Address + " on their port number " + RemoteIpEndPoint.Port);

				udpClient.Close();
				udpClientB.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
	}
}
