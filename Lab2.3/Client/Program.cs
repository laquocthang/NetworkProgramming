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
					Console.Write("Your input: ");
					string message = Console.ReadLine();
					if (message.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
					{
						return;
					}
					if (!ThangClient.SendToServer(message, out response))
					{
						Console.WriteLine("** ERROR: " + response);
						break;
					}
					if (ThangClient.ReceiveFromServer(out response))
					{
						Console.WriteLine("\nServer: " + response);
					}
					else
					{
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
