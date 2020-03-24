using System;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			Server ThangServer = new Server();
			string response;
			ThangServer.ListenToClient();
			while (true)
			{
				if (ThangServer.ReceiveFromClient(out response))
				{
					Console.WriteLine("Client: " + response);
				}
				else
				{
					Console.WriteLine("** ERROR: " + response);
					break;
				}
				Console.Write("Your input: ");
				string message = Console.ReadLine();
				if (!ThangServer.SendToClient(message, out response))
				{
					Console.WriteLine("** ERROR: " + response);
					break;
				}
			}
			Console.ReadKey();
		}
	}
}
