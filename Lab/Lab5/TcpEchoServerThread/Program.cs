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
			if (args.Length != 1)
				throw new AggregateException("Missing parameters!");
			int port = int.Parse(args[0]);
			TcpListener listener = new TcpListener(IPAddress.Any, port);
			ILogger logger = new ConsoleLogger();
			listener.Start();
			while (true)
			{
				try
				{
					Socket client = listener.AcceptSocket();
					EchoProtocol protocol = new EchoProtocol(client, logger);
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
