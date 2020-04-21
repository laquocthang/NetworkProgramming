using System;
using System.Net;
using System.Text;

namespace Bai1
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			//Console.Write("Nhập tên miền: ");
			//string host = Console.ReadLine();
			//Console.WriteLine(GetHostInfo(host));
			foreach (var arg in args)
			{
				Console.WriteLine(GetHostInfo(arg));
			}
			Console.ReadKey();
		}

		/// <summary>
		/// Phân giải tên miền
		/// </summary>
		/// <param name="host"></param>
		public static string GetHostInfo(string host)
		{
			StringBuilder builder = new StringBuilder();
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(host);
				builder.AppendLine("Tên miền: " + hostEntry.HostName);
				builder.Append("Địa chỉ IP: ");
				foreach (IPAddress item in hostEntry.AddressList)
				{
					builder.AppendLine(item.ToString() + " ");
				}
			}
			catch (Exception)
			{
				builder.AppendLine("Không phân giải được tên miền: " + host);
			}
			return builder.ToString();
		}
	}
}
