using System.Collections;

namespace TcpEchoServerThread
{
	public interface ILogger
	{
		public void Write(ArrayList entry);
		public void Write(string entry);
	}
}
