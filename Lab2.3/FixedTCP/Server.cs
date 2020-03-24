using FixedTCP;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	public class Server
	{
		private byte[] buff;
		private int bytes;
		private IPEndPoint serverEndPoint;
		private EndPoint clientEndPoint;
		private Socket serverSocket;
		private Socket clientSocket;

		/// <summary>
		/// Initializes server EndPoint and server socket
		/// </summary>
		public Server()
		{
			serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			serverSocket.Bind(serverEndPoint);
		}

		/// <summary>
		/// Listens and assigns socket to the newly created client
		/// </summary>
		public void ListenToClient()
		{
			serverSocket.Listen(10);
			clientSocket = serverSocket.Accept();
			clientEndPoint = clientSocket.RemoteEndPoint;
			Console.WriteLine(clientEndPoint.ToString());
		}

		/// <summary>
		/// Returns response from client
		/// </summary>
		/// <param name="response">If client can't connect, the response is error message</param>
		/// <returns>Is client connectable?</returns>
		public bool ReceiveFromClient(out string response)
		{
			buff = Receive.ReceiveVarData(clientSocket, out response);
			if (response != null)
			{
				return false;
			}
			response = Encoding.UTF8.GetString(buff);
			return true;
		}

		/// <summary>
		/// Sends message to client
		/// </summary>
		/// <param name="message">Your message which you would like to send to client</param>
		/// <param name="response">If client can't connect, the response is error message</param>
		/// <returns>Is client connectable?</returns>
		public bool SendToClient(string message, out string response)
		{
			buff = Encoding.UTF8.GetBytes(message);
			bytes = Send.SendVarData(clientSocket, buff, out response);
			if (response != null)
			{
				return false;
			}
			return true;
		}
	}
}
