using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Bai2
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine(ShowNetworkInfo());
			Console.ReadKey();
		}

		/// <summary>
		/// Hiển thị thông tin các cạc mạng trong máy tính
		/// </summary>
		public static string ShowNetworkInfo()
		{
			StringBuilder builder = new StringBuilder();
			foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
			{
				builder.AppendLine("Tên Adapter: " + adapter.Description);
				foreach (UnicastIPAddressInformation addressInformation in adapter.GetIPProperties().UnicastAddresses)
				{
					if (addressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
					{
						builder.AppendLine("\tĐịa chỉ IP:. . . . . .\t" + addressInformation.Address.ToString());
						builder.AppendLine("\tSubnet mask:. . . . . \t" + addressInformation.IPv4Mask);
					}
				}
				var gateway = adapter.GetIPProperties().GatewayAddresses;
				if (gateway.Count > 0)
				{
					foreach (var item in gateway)
					{
						builder.AppendLine("\tDefault gateway:. . . .\t" + item.Address.ToString());
					}
				}
				builder.AppendLine();
			}
			return builder.ToString();
		}
	}
}
