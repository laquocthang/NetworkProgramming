using System;
using System.Collections;
using System.Net.Sockets;
using System.Threading;

namespace TcpEchoServerThread
{
	public class EchoProtocol : IProtocol
	{
		private NetworkStream stream;
		private ILogger logger;

		public const int BUFF_SIZE = 32;

		public EchoProtocol(NetworkStream stream, ILogger logger)
		{
			this.stream = stream;
			this.logger = logger;
		}

		public void HandleClient()
		{
			ArrayList entry = new ArrayList();
			entry.Add("Client address and port: " + stream.ToString());
			entry.Add("Thread: " + Thread.CurrentThread.GetHashCode());
			try
			{
				int receivedMessageSize; ;
				int totalBytes = 0;
				byte[] buff = new byte[BUFF_SIZE];
				try
				{
					while ((receivedMessageSize = stream.Read(buff, 0, buff.Length)) != 0)
					{
						stream.Write(buff, 0, receivedMessageSize);
						totalBytes += receivedMessageSize;
					}
				}
				catch (SocketException e)
				{
					entry.Add(e.ErrorCode + " error: " + e.Message);
				}
				entry.Add("Echoed " + totalBytes + " bytes");
			}
			catch (SocketException e)
			{
				entry.Add(e.ErrorCode + " error: " + e.Message);
			}
			stream.Close();
			logger.Write(entry);
		}
	}
}
