using System;
using System.Net;

namespace Bai1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Nhap ten mien: ");
			string host = Console.ReadLine();
			GetHostInfo(host);
			Console.ReadKey();
		}

		private static void GetHostInfo(string host)
		{
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(host);
				Console.WriteLine("Ten mien: " + hostEntry.HostName);
				Console.WriteLine("Dia chi IP: ");
				foreach (IPAddress item in hostEntry.AddressList)
				{
					Console.WriteLine(item.ToString() + " ");
				}
				Console.WriteLine();
			}
			catch (Exception)
			{
				Console.WriteLine("Khong phan giai duoc ten mien: " + host);
				//throw;
			}
		}
	}
}
