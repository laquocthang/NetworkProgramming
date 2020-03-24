using FixedTCP;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
	public class Client
	{
		private byte[] buff;
		private int bytes;
		private IPEndPoint serverEndPoint;
		private Socket serverSocket;

		public Client()
		{
			serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		public bool ConnectToServer(out string response)
		{
			try
			{
				serverSocket.Connect(serverEndPoint);
			}
			catch (SocketException e)
			{
				response = e.Message;
				return false;
			}
			response = null;
			return true;
		}

		public bool SendToServer(string message, out string response)
		{
			buff = Encoding.UTF8.GetBytes(message);
			bytes = Send.SendVarData(serverSocket, buff, out response);
			if (response != null)
			{
				return false;
			}
			return true;
		}

		public bool ReceiveFromServer(out string response)
		{
			buff = Receive.ReceiveVarData(serverSocket, out response);
			if (response != null)
			{
				return false;
			}
			response = Encoding.UTF8.GetString(buff);
			return true;
		}
	}
}
