using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDPLibrary;

namespace UDPClient
{
	class Program
	{
		static void Main(string[] args)
		{
			NewUDPClient client = new NewUDPClient();
			client.Run();
			Console.ReadKey();
		}
	}
}
