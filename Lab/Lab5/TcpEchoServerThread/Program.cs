using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpEchoServerThread
{
	class Program
	{
		static void Main(string[] args)
		{
			RunInCMD(9000);
		}

		private static void RunInCMD(string[] args)
		{
			if (args.Length != 1)
				throw new AggregateException("Missing parameter!");
			int port = int.Parse(args[0]);
			TcpListener listener = new TcpListener(IPAddress.Any, port);
			ILogger logger = new ConsoleLogger();
			listener.Start();
			while (true)
			{
				try
				{
					TcpClient client = listener.AcceptTcpClient();
					EchoProtocol protocol = new EchoProtocol(client.GetStream(), logger);
					Thread thread = new Thread(protocol.HandleClient);
					thread.Start();
					logger.Write("Created and started thread " + thread.GetHashCode());
				}
				catch (IOException e)
				{
					logger.Write("Error: " + e.Message);
				}
			}
		}

		private static void RunInCMD(int port)
		{
			TcpListener listener = new TcpListener(IPAddress.Any, port);
			ILogger logger = new ConsoleLogger();
			listener.Start();
			while (true)
			{
				try
				{
					TcpClient client = listener.AcceptTcpClient();
					EchoProtocol protocol = new EchoProtocol(client.GetStream(), logger);
					Thread thread = new Thread(protocol.HandleClient);
					thread.Start();
					logger.Write("Created and started thread " + thread.GetHashCode());
				}
				catch (IOException e)
				{
					logger.Write("Error: " + e.Message);
				}
			}
		}

	}
}
