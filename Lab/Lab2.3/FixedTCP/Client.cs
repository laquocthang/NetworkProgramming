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

		/// <summary>
		/// Initializes server EndPoint and server socket
		/// </summary>
		public Client()
		{
			serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		/// <summary>
		/// Try to connect to server
		/// </summary>
		/// <param name="response">If the server can't connect, the response is error message</param>
		/// <returns>Is server connectable?</returns>
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

		/// <summary>
		/// Sends message to server
		/// </summary>
		/// <param name="message">Your message which you would like to send to server</param>
		/// <param name="response">If server can't connect, the response is error message</param>
		/// <returns>Is server connectable?</returns>
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

		/// <summary>
		/// Returns response from server
		/// </summary>
		/// <param name="response">If server can't connect, the response is error message</param>
		/// <returns>Is server connectable?</returns>
		public bool ReceiveFromServer(out string response)
		{
			buff = Receive.ReceiveVarData(serverSocket, out response);
			if (response != null)	//Response lỗi
			{
				return false;
			}
			response = Encoding.UTF8.GetString(buff); //Response tin nhắn
			return true;
		}
	}
}
