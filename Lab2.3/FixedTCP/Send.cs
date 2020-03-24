using System;
using System.Net.Sockets;

namespace FixedTCP
{
	public class Send
	{
		private static int SendData(Socket s, byte[] data)
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

		private static int SendVarData(Socket s, byte[] buff)
		{
			int total = 0;
			int size = buff.Length;
			int dataleft = size;
			int sent;
			byte[] datasize = new byte[4];
			datasize = BitConverter.GetBytes(size);
			sent = s.Send(datasize);
			while (total < size)
			{
				sent = s.Send(buff, total, dataleft, SocketFlags.None);
				total += sent; dataleft -= sent;
			}
			return total;
		}
	}
}
