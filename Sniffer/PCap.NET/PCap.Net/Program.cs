using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCap.Net
{
	class Program
	{
		static void Main(string[] args)
		{
			IList<LivePacketDevice> devices = LivePacketDevice.AllLocalMachine;

			Sniffer();

			Console.ReadKey();
		}

		private static string GetDetailList(IList<LivePacketDevice> devices)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var device in devices)
			{
				builder.AppendLine(GetDetail((LivePacketDevice)device));
			}
			return builder.ToString();
		}

		private static string GetList(IList<LivePacketDevice> devices)
		{
			StringBuilder builder = new StringBuilder();
			int i = 1;
			foreach (var device in devices)
			{
				builder.Append(i + ". " + device.Name);
				if (device.Description != null)
					builder.AppendLine(" (" + device.Description + ")");
				else
					builder.AppendLine(" (No description available)");
				i++;
			}
			return builder.ToString();
		}

		private static string GetDetail(IPacketDevice device)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(device.Name);

			if (device.Description != null)
				builder.AppendLine("\tDescription: " + device.Description);

			builder.AppendLine("\tLoopback: " + (((device.Attributes & DeviceAttributes.Loopback) == DeviceAttributes.Loopback) ? "yes" : "no"));

			foreach (var address in device.Addresses)
			{
				builder.AppendLine("\tAddress Family: " + address.Address.Family);

				if (address.Address != null)
					builder.AppendLine(("\tAddress: " + address.Address));
				if (address.Netmask != null)
					builder.AppendLine(("\tNetmask: " + address.Netmask));
				if (address.Broadcast != null)
					builder.AppendLine(("\tBroadcast Address: " + address.Broadcast));
				if (address.Destination != null)
					builder.AppendLine(("\tDestination Address: " + address.Destination));
			}
			builder.AppendLine();

			return builder.ToString();
		}

		private static void Sniffer()
		{
			IList<LivePacketDevice> devices = LivePacketDevice.AllLocalMachine;

			if (devices.Count == 0)
			{
				Console.WriteLine("No interfaces found! Make sure WinPcap is installed.");
				return;
			}

			Console.WriteLine(GetList(devices));

			PacketDevice selectedDevice = ChooseDeviceFromList(devices);

			// Open the device
			using (PacketCommunicator communicator =
				selectedDevice.Open(65536,                                  // portion of the packet to capture
																			// 65536 guarantees that the whole packet will be captured on all the link layers
									PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
									1000))                                  // read timeout
			{
				Console.WriteLine("Listening on " + selectedDevice.Description + "...");

				// communicator.ReceivePackets(0, PacketHandler);

				CaptureWithoutCallback(communicator);

				return;
			}
		}

		private static void CaptureWithoutCallback(PacketCommunicator communicator)
		{
			Packet packet;
			do
			{
				PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
				switch (result)
				{
					case PacketCommunicatorReceiveResult.Timeout:
						// Timeout elapsed
						continue;
					case PacketCommunicatorReceiveResult.Ok:
						Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" +
										  packet.Length);
						break;
					default:
						throw new InvalidOperationException("The result " + result + " shoudl never be reached here");
				}
			} while (true);
		}

		private static PacketDevice ChooseDeviceFromList(IList<LivePacketDevice> devices)
		{
			int deviceIndex = 0;
			do
			{
				Console.Write("Enter the interface number (1 - " + devices.Count + "): ");
				string deviceIndexString = Console.ReadLine();
				if (!int.TryParse(deviceIndexString, out deviceIndex) || deviceIndex < 1 || deviceIndex > devices.Count)
				{
					deviceIndex = 0;
				}
			}
			while (deviceIndex == 0);

			return devices[deviceIndex - 1];
		}

		private static void PacketHandler(Packet packet)
		{
			Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length);
		}
	}
}
