using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace TcpEchoServerThread
{
	public class EchoProtocol : IProtocol
	{
		private Socket clientSocket;
		private ILogger logger;

		public const int BUFF_SIZE = 32;

		public EchoProtocol(Socket clientSocket, ILogger logger)
		{
			this.clientSocket = clientSocket;
			this.logger = logger;
		}

		public void HandleClient()
		{
			ArrayList entry = new ArrayList();
			entry.Add("Client address and port: " + clientSocket.RemoteEndPoint);
			entry.Add("Thread: " + Thread.CurrentThread.GetHashCode());
			try
			{
				int receivedMessageSize; ;
				int totalBytes = 0;
				byte[] buff = new byte[BUFF_SIZE];
				try
				{
					while ((receivedMessageSize = clientSocket.Receive(buff, 0, buff.Length, SocketFlags.None)) != 0)
					{
						clientSocket.Send(buff, 0, receivedMessageSize, SocketFlags.None);
						totalBytes += receivedMessageSize;
					}
				}
				catch (SocketException e)
				{
					entry.Add(e.ErrorCode + " error: " + e.Message);
				}
				catch (IOException e)
				{
					entry.Add("Error: " + e.Message);
				}
				entry.Add("Echoed " + totalBytes + " bytes");
			}
			catch (SocketException e)
			{
				entry.Add(e.ErrorCode + " error: " + e.Message);
			}
			clientSocket.Close();
			logger.Write(entry);
		}
	}
}
