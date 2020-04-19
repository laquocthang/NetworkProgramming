using ClassData;
using System;
using System.Net.Sockets;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Run01();
			Console.ReadKey();
		}

		static void Run01()
		{
			Employee employee = new Employee(1, "La Quoc", "Thang", 10, 15000000);
			TcpClient client;
			try
			{
				client = new TcpClient("127.0.0.1", 9000);
			}
			catch (SocketException e)
			{
				Console.WriteLine("Can't connect to server");
				Console.WriteLine("Error: " + e.Message);
				return;
			}
			NetworkStream stream = client.GetStream();
			byte[] data = employee.GetBytes();
			int size = employee.Size;
			byte[] packageSize = new byte[2];
			Console.WriteLine("Size of package = {0}", size);
			// Send the size of package
			packageSize = BitConverter.GetBytes(size);
			stream.Write(packageSize, 0, 2);
			// Send the data
			stream.Write(data, 0, size);

			stream.Flush();
			stream.Close();
			client.Close();
		}

		private static void Run02()
		{
			Employee employee = new Employee();
			TcpClient client;
			try
			{
				client = new TcpClient("127.0.0.1", 9000);
			}
			catch (SocketException e)
			{
				Console.WriteLine("Can't connect to server");
				Console.WriteLine("Error: " + e.Message);
				return;
			}
			NetworkStream stream = client.GetStream();

			while (true)
			{
				employee.ImportEmployee();
				byte[] data = employee.GetBytes();
				int size = employee.Size;
				byte[] packageSize = new byte[2];
				Console.WriteLine("Size of package = {0}", size);
				// Send the size of package
				packageSize = BitConverter.GetBytes(size);
				stream.Write(packageSize, 0, 2);
				// Send the data
				stream.Write(data, 0, size);
				Console.Write("Do you want to continue? Type 'no' to exit: ");
				string ans = Console.ReadLine();
				if (ans.Equals("no", StringComparison.InvariantCultureIgnoreCase))
					break;
			}
			stream.Flush();
			stream.Close();
			client.Close();
		}
	}
}
