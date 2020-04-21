using PcapDotNet.Core;
using System;
using System.Collections.Generic;

namespace PCap.Net
{
	class Program
	{
		static void Main(string[] args)
		{
			IList<LivePacketDevice> devices = LivePacketDevice.AllLocalMachine;

			Lib pcap = new Lib(LivePacketDevice.AllLocalMachine);
			pcap.Sniffer();

			Console.ReadKey();
		}


	}
}
