using System;
using System.Net.Sockets;

namespace FixedTCP
{
	public class Send
	{
		/// <summary>
		/// Gửi thông điệp có kích thước cố định
		/// </summary>
		/// <param name="s"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static int SendData(Socket s, byte[] data)
		{
			int total = 0;
			int size = data.Length;
			int dataLeft;
			int send;
			dataLeft = size;
			while (total < size)
			{
				send = s.Send(data, total, dataLeft, SocketFlags.None);
				total += send;
				dataLeft -= send;
			}
			return total;
		}

		/// <summary>
		/// Gửi kèm kích thước thông điệp cùng với thông điệp
		/// </summary>
		/// <param name="s"></param>
		/// <param name="buff"></param>
		/// <returns></returns>
		public static int SendVarData(Socket s, byte[] buff, out string response)
		{
			int total = 0;
			int size = buff.Length;
			int dataleft = size;
			int sent;
			byte[] datasize = new byte[4];
			datasize = BitConverter.GetBytes(size);
			try
			{
				sent = s.Send(datasize);
			}
			catch (SocketException e)
			{
				response = e.Message;
				return 0;
			}
			while (total < size)
			{
				sent = s.Send(buff, total, dataleft, SocketFlags.None);
				total += sent;
				dataleft -= sent;
			}
			response = null;
			return total;
		}
	}
}
