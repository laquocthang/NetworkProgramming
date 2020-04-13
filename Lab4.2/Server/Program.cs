using ClassData;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			Run01();
			Console.ReadKey();
		}

		private static void Run01()
		{
			byte[] data = new byte[1024];
			int byteReceived;
			TcpListener server = new TcpListener(IPAddress.Any, 9000);
			server.Start();

			TcpClient client = server.AcceptTcpClient();
			NetworkStream stream = client.GetStream();
			// Read the size of package
			byte[] size = new byte[2];
			byteReceived = stream.Read(size, 0, 2);
			int packageSize = BitConverter.ToInt16(size, 0);
			Console.WriteLine("The size of package: {0}", packageSize);
			// Read the data
			byteReceived = stream.Read(data, 0, packageSize);
			Employee employee = new Employee(data);
			Console.WriteLine(employee.ToString());

			stream.Close();
			client.Close();
			server.Stop();
		}
	}
}
