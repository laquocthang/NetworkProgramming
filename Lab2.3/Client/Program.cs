using System;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Client ThangClient = new Client();
			string response;
			Console.WriteLine("Connecting to server...");
			if (ThangClient.ConnectToServer(out response))
			{
				Console.WriteLine("Connected successfully");
				while (true)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("You: ");
					string message = Console.ReadLine();
					if (message.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
					{
						return;
					}
					if (!ThangClient.SendToServer(message, out response))
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("** ERROR: " + response);
						break;
					}
					Console.ForegroundColor = ConsoleColor.White;
					if (ThangClient.ReceiveFromServer(out response))
					{
						Console.WriteLine("Server: " + response);
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("** ERROR: " + response);
						break;
					}
				}
			}
			else
			{
				Console.WriteLine(response);
			}
			Console.ReadKey();
		}
	}
}
