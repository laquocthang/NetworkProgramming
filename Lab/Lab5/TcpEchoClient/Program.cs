﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpEchoClient
{
	class Program
	{
		static void Main(string[] args)
		{
			RunInCMD(new string[] { "127.0.0.1", "9000", "La Quoc Thang" });
			Console.ReadKey();
		}

		private static void RunInCMD(string[] args)
		{
			if (args.Length != 3)
			{
				Console.WriteLine("Missing parameters!");
				return;
			}

			int port;
			string message;
			try
			{
				port = int.Parse(args[1]);
				message = args[2];
			}
			catch (Exception)
			{
				Console.WriteLine("Invalid parameters!");
				return;
			}

			TcpClient client;
			try
			{
				client = new TcpClient(args[0], port);
			}
			catch (SocketException e)
			{
				Console.WriteLine("Can't connect to server");
				Console.WriteLine("Error: " + e.Message);
				return;
			}

			NetworkStream stream = client.GetStream();

			while (true)
			{
				try
				{
					byte[] buff = Encoding.UTF8.GetBytes(message);
					stream.Write(buff, 0, buff.Length);

					buff = new byte[1024];
					int bytes = stream.Read(buff, 0, buff.Length);
					message = Encoding.UTF8.GetString(buff, 0, bytes);
					Console.WriteLine("Server: " + message);

					Thread.Sleep(1000);
				}
				catch (Exception)
				{
					Console.WriteLine("Has error when processing this request");
					break;
				}
			}

			stream.Flush();
			stream.Close();
			client.Close();
		}

	}
}
