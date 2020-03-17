using System;
using System.Net;
using System.Text;

namespace Bai1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.Write("Nhập tên miền: ");
			string host = Console.ReadLine();
			GetHostInfo(host);
			Console.ReadKey();
		}

		/// <summary>
		/// Phân giải tên miền
		/// </summary>
		/// <param name="host"></param>
		private static void GetHostInfo(string host)
		{
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(host);
				Console.WriteLine("Tên miền: " + hostEntry.HostName);
				Console.Write("Địa chỉ IP: ");
				foreach (IPAddress item in hostEntry.AddressList)
				{
					Console.WriteLine(item.ToString() + " ");
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Không phân giải được tên miền: " + host);
				//throw;
			}
		}
	}
}
