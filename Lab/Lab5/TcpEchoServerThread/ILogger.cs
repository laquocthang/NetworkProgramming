using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TcpEchoServerThread
{
	public interface ILogger
	{
		public void Write(ArrayList entry);
		public void Write(string entry);
	}
}
