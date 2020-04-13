using Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient1
{
	// client side
	class Program
	{
		static void Main(string[] args)
		{
			Connect("127.0.0.1", 11000);
			Console.ReadKey();
		}

		static void Connect(string server, int port)
		{
			UdpClient udpClient = new UdpClient();
			try
			{
				udpClient.Connect(server, port);
				byte[] data;
				EmployeeModel employee;
				//IPEndPoint object will allow us to read datagrams sent from any source.
				IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

				while (true)
				{
					employee = new EmployeeModel();

					employee.ImportEmployee();
					data = employee.GetBytes();
					int size = employee.Size;
					byte[] packageSize = new byte[2];
					Console.WriteLine("Size of package = {0}", size);

					packageSize = BitConverter.GetBytes(size);
					udpClient.Send(packageSize, packageSize.Length);

					udpClient.Send(data, size);

					Console.Write("Do you want to continue? Type 'no' to exit: ");
					string ans = Console.ReadLine();
					if (ans.Equals("no", StringComparison.InvariantCultureIgnoreCase))
					{
						packageSize = BitConverter.GetBytes(0);
						udpClient.Send(packageSize, 0);
						break;
					}
				}
				udpClient.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
		}
	}
}
