using System;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			Server ThangServer = new Server();
			string response;
			Console.WriteLine("Connecting to client...");
			Console.WriteLine("Connected client info: " + ThangServer.ListenToClient());
		start:
			while (true)
			{
				Console.ForegroundColor = ConsoleColor.White;
				if (ThangServer.ReceiveFromClient(out response))
				{
					if (response.Equals("calculate", StringComparison.InvariantCultureIgnoreCase))
					{
						if (ThangServer.Calculate(out response))
							goto start;
					}
					Console.WriteLine("Client: " + response);
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("** ERROR: " + response);
					break;
				}
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("You: ");
				string message = Console.ReadLine();
				if (!ThangServer.SendToClient(message, out response))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("** ERROR: " + response);
					break;
				}
			}
			//while (true)
			//{
			//	if (ThangServer.Calculate(out response))
			//		break;
			//}
			Console.ReadKey();
		}
	}
}
