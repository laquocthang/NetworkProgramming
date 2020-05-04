using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab6
{
	public partial class Server : Form
	{
		string firstMessage = "Hello Client";
		int maxSize = 1024;
		int port = 9000;
		byte[] buff;
		int bytes;
		Socket client;
		Socket server;
		IPEndPoint serverEndPoint;

		public Server()
		{
			InitializeComponent();
			Form.CheckForIllegalCrossThreadCalls = false;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			serverEndPoint = new IPEndPoint(IPAddress.Any, port);
			server.Bind(serverEndPoint);
			server.Listen(5);
			server.BeginAccept(new AsyncCallback(AcceptCallback), server);
			tbxStatus.Text = "Waiting for connecting ...";
		}

		private void AcceptCallback(IAsyncResult ar)
		{
			Socket server = (Socket)ar.AsyncState;
			client = server.EndAccept(ar);
			EndPoint remote = client.RemoteEndPoint;
			tbxStatus.Text = remote.ToString();
			buff = Encoding.UTF8.GetBytes(firstMessage);
			client.BeginSend(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
		}

		private void SendCallback(IAsyncResult ar)
		{
			Socket server = (Socket)ar.AsyncState;
			server.EndSend(ar);
			buff = new byte[maxSize];
			server.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), server);
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			Socket server = (Socket)ar.AsyncState;
			try
			{
				bytes = server.EndReceive(ar);
			}
			catch (Exception)
			{
				ShowText("Error occurs when receiving data");
				btnStop.PerformClick();
				return;
			}
			if (bytes == 0)
			{
				ShowText("Client disconnected");
				btnStop.PerformClick();
			}
			else
			{
				string message = Encoding.UTF8.GetString(buff, 0, bytes);
				ShowText(message);
				server.BeginSend(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(SendCallback), server);
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			client.Close();
			server.Close();
		}

		private void ShowText(string text)
		{
			tbxReceive.Text = tbxReceive.Text + text + "\r\n";
		}

		private void Server_FormClosing(object sender, FormClosingEventArgs e)
		{
			btnStop.PerformClick();
		}
	}
}
