﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleUDPClient
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
			EndPoint remote = (EndPoint)sender;

			//socket.Connect(remote);

			byte[] buff;
			string message;
			
			while (true)
			{
				//Send the message to server
				Console.Write("\nWrite the message: ");
				message = Console.ReadLine();
				buff = Encoding.ASCII.GetBytes(message);
				Console.WriteLine("Sending this message to server...");
				socket.SendTo(buff, server);

				//Receive the message from server
				int bytes;
				buff = new byte[10];
				try
				{
					bytes = socket.ReceiveFrom(buff, ref remote);
					message = Encoding.ASCII.GetString(buff, 0, bytes);
					Console.WriteLine("Received the message from server: " + message);
				}
				catch (SocketException)
				{
					Console.WriteLine("The data is missing, please retry!");
					//throw;
				}

				Console.Write("\nDo you want to exit? Type \"exit\" to close client, \"exit all\" to close all: ");
				string command = Console.ReadLine();
				if (command == "exit")
					break;
				else if (command == "exit all")
				{
					socket.Close();
					break;
				}
			}
			
			/*
			Console.WriteLine("Sending 5 messages to server");
			for (int i = 1; i <= 5; i++)
			{
				buff = Encoding.ASCII.GetBytes("Thong diep: " + i.ToString());
				socket.SendTo(buff, buff.Length, SocketFlags.None, server);
			}
			Console.WriteLine("Sended");
			*/
			Console.ReadKey();
		}
	}
}
