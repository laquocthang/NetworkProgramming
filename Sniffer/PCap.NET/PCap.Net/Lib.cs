using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCap.Net
{
	public class Lib
	{
		private IList<LivePacketDevice> devices;

		public Lib(IList<LivePacketDevice> devices)
		{
			this.devices = devices;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			foreach (var device in devices)
			{
				builder.AppendLine(GetDetail((LivePacketDevice)device));
			}
			return builder.ToString();
		}

		/// <summary>
		/// Gets string that contains list of network interface
		/// </summary>
		/// <param name="devices"></param>
		/// <returns></returns>
		public string ToSimpleString()
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

		/// <summary>
		/// Gets information about a network interface
		/// </summary>
		/// <param name="device"></param>
		/// <returns></returns>
		public string GetDetail(IPacketDevice device)
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

		/// <summary>
		/// Capture packet from a interface
		/// </summary>
		public void Sniffer()
		{
			if (devices.Count == 0)
			{
				throw new AggregateException("No interfaces found! Make sure WinPcap is installed.");
			}

			PacketDevice selectedDevice = ChooseDeviceFromList();

			// Open the device
			using (PacketCommunicator communicator =
				selectedDevice.Open(65536,                                  // portion of the packet to capture
																			// 65536 guarantees that the whole packet will be captured on all the link layers
									PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
									1000))                                  // read timeout
			{
				Console.WriteLine("Listening on " + selectedDevice.Description + "...");

				communicator.ReceivePackets(0, PacketHandler);

				// CaptureWithoutCallback(communicator);
			}
		}

		/// <summary>
		/// Captures packets
		/// We support only Ethernet, not Wifi.
		/// </summary>
		public void SnifferWithFilter(string filter = "ip and tcp")
		{
			IList<LivePacketDevice> devices = LivePacketDevice.AllLocalMachine;

			if (devices.Count == 0)
			{
				throw new AggregateException("No interfaces found! Make sure WinPcap is installed.");
			}

			PacketDevice selectedDevice = ChooseDeviceFromList();

			// Open the device
			using (PacketCommunicator communicator =
				selectedDevice.Open(65536,                                  // portion of the packet to capture
																			// 65536 guarantees that the whole packet will be captured on all the link layers
									PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
									1000))                                  // read timeout
			{
				Console.WriteLine("Listening on " + selectedDevice.Description + "...");

				// Set the filter
				communicator.SetFilter(filter);

				communicator.ReceivePackets(0, PacketHandler);

				// CaptureWithoutCallback(communicator);
			}
		}

		/// <summary>
		/// Saves packets to a dump file
		/// </summary>
		/// <param name="communicator"></param>
		/// <param name="filename"></param>
		public void CaptureToDumpFile(PacketCommunicator communicator, string filename)
		{
			// Open the dump file
			using (PacketDumpFile dumpFile = communicator.OpenDump(filename))
			{
				// start the capture
				communicator.ReceivePackets(0, dumpFile.Dump);
			}
		}

		/// <summary>
		/// Starts the capture
		/// </summary>
		/// <param name="communicator"></param>
		public void CaptureWithoutCallback(PacketCommunicator communicator)
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

		/// <summary>
		/// Shows a list of network interface and enable user to choose
		/// </summary>
		/// <param name="devices"></param>
		/// <returns></returns>
		public PacketDevice ChooseDeviceFromList()
		{
			int deviceIndex = 0;

			// Print the list
			Console.WriteLine(ToSimpleString());
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

			// Take the selected adapter
			return devices[deviceIndex - 1];
		}

		/// <summary>
		/// Callback function invoked by libpcap for every incoming packet
		/// </summary>
		/// <param name="packet"></param>
		private void PacketHandler(Packet packet)
		{
			// print timestamp and length of the packet
			Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length);

			IpV4Datagram ip = packet.Ethernet.IpV4;
			UdpDatagram udp = ip.Udp;

			// print ip addresses and udp ports
			Console.WriteLine(ip.Source + "\t:\t" + udp.SourcePort + " \t->\t " + ip.Destination + "\t:\t" + udp.DestinationPort);
		}

		public void ReadDumpFile()
		{
			// https://github.com/PcapDotNet/Pcap.Net/wiki/Pcap.Net-Tutorial-Handling-offline-dump-files
		}
	}
}
