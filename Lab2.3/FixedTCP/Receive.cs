using System;
using System.Net.Sockets;
using System.Text;

namespace FixedTCP
{
	public class Receive
	{
		private static byte[] ReceiveData(Socket s, int size)
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

		private static byte[] ReceiveVarData(Socket s)
		{
			int total = 0;
			int recv;
			byte[] datasize = new byte[4];
			recv = s.Receive(datasize, 0, 4, 0);
			int size = BitConverter.ToInt32(datasize, 0);
			int dataleft = size;
			byte[] data = new byte[size];
			while (total < size)
			{
				recv = s.Receive(data, total, dataleft, 0);
				if (recv == 0)
				{
					data = Encoding.ASCII.GetBytes("exit "); break;
				}
				total += recv; dataleft -= recv;
			}
			return data;
		}
	}
}
