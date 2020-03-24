using System;
using System.Net.Sockets;
using System.Text;

namespace FixedTCP
{
	public class Receive
	{
		/// <summary>
		/// Nhận thông điệp có kích thước cố định
		/// </summary>
		/// <param name="s"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static byte[] ReceiveData(Socket s, int size)
		{
			int total = 0;
			int receive;
			int dataLeft = size;
			byte[] data = new byte[size];
			while (total < size)
			{
				receive = s.Receive(data, total, dataLeft, SocketFlags.None);
				if (receive == 0)
				{
					data = Encoding.ASCII.GetBytes("Exit");
				}
				total += receive;
				dataLeft -= receive;
			}
			return data;
		}

		/// <summary>
		/// Nhận kèm kích thước thông điệp cùng với thông điệp
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static byte[] ReceiveVarData(Socket s, out string response)
		{
			int total = 0;
			int recv;
			byte[] datasize = new byte[4];
			try
			{
				recv = s.Receive(datasize, 0, 4, 0);
			}
			catch (SocketException e)
			{
				response = e.Message;
				return null;
			}
			int size = BitConverter.ToInt32(datasize, 0);
			int dataleft = size;
			byte[] data = new byte[size];
			while (total < size)
			{
				recv = s.Receive(data, total, dataleft, 0);
				total += recv;
				dataleft -= recv;
				if (recv == 0)
				{
					data = Encoding.ASCII.GetBytes("Exit");
					break;
				}
			}
			response = null;
			return data;
		}
	}
}
