using Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient2
{
	// server side
	class Program
	{
		static void Main(string[] args)
		{
			Connect(11000);
			Console.ReadKey();
		}

		static void Connect(int port)
		{
			byte[] data;
			byte[] size = new byte[2];
			UdpClient udpClient = new UdpClient(port);
			IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
			Console.WriteLine("Waiting for a connection...");

			while (true)
			{
				data = new byte[1024];
				size = udpClient.Receive(ref remote);
				if (size.Length == 0)
					break;
				int packageSize = BitConverter.ToInt16(size, 0);
				Console.WriteLine("The size of package: {0}", packageSize);

				data = udpClient.Receive(ref remote);
				EmployeeModel employee = new EmployeeModel(data);
				Console.WriteLine(employee.ToString());
			}
			udpClient.Close();
		}
	}
}