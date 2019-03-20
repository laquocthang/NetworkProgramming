using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Bai2
{
	class Program
	{
		static void Main(string[] args)
		{
			foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
			{
				Console.WriteLine(adapter.Description); //Write name of adapter
				foreach (UnicastIPAddressInformation addressInformation in adapter.GetIPProperties().UnicastAddresses)
				{
					if (addressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
					{
						Console.WriteLine("\tIP Address:. . . . . .\t" + addressInformation.Address.ToString());
						Console.WriteLine("\tSubnet mask:. . . . . \t" + addressInformation.IPv4Mask);
					}
				}
				var gateway = adapter.GetIPProperties().GatewayAddresses;
				if (gateway.Count > 0)
				{
					foreach (var item in gateway)
					{
						Console.WriteLine("\tDefault gateway:. . . .\t" + item.Address.ToString());
					}
				}
				Console.WriteLine();
			}
			Console.ReadKey();
		}
	}
}
