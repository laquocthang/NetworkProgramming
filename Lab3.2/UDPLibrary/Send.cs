using System;
using System.Net;
using System.Net.Sockets;

namespace UDPLibrary
{
	public class Send
	{

		/// <summary>
		/// Send and wait for a response from client
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public static int SendData(byte[] message, Socket socket, EndPoint remote)
		{
			int receive;
			int retry = 0;

			while (true)
			{
				Console.WriteLine("Truyen lai lan thu #{0}", retry);
				try
				{
					socket.SendTo(message, message.Length, SocketFlags.None, remote);
					byte[] temp = new byte[1024];
					receive = socket.ReceiveFrom(temp, ref remote);
				}
				catch (SocketException)
				{
					receive = 0;
				}
				if (receive > 0)
				{
					return receive;
				}
				else
				{
					retry++;
					if (retry > 4)
						return 0;
				}
			}
		}
	}
}
