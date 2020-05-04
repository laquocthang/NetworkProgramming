using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
	public partial class Client : Form
	{
		int port = 9000;
		int maxSize = 1024;
		byte[] buff;
		int bytes;
		Socket server;
		IPEndPoint serverEndPoint;

		public Client()
		{
			InitializeComponent();
			Form.CheckForIllegalCrossThreadCalls = false;
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			tbxStatus.Text = "Connecting ... ";
			server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			serverEndPoint = new IPEndPoint(IPAddress.Loopback, port);
			server.BeginConnect(serverEndPoint, new AsyncCallback(ConnectCallback), server);
		}

		private void ConnectCallback(IAsyncResult ar)
		{
			Socket server = (Socket)ar.AsyncState;
			try
			{
				tbxStatus.Text = "Connecting ... ";
				server.EndConnect(ar);
			}
			catch (Exception)
			{
				ShowText("Error occurs when connecting to server");
				return;
			}
			buff = new byte[maxSize];
			server.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), server);
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			Socket server = (Socket)ar.AsyncState;
			bytes = server.EndReceive(ar);
			string message = Encoding.UTF8.GetString(buff, 0, bytes);
			ShowText(message);
		}

		private void SendCallback(IAsyncResult ar)
		{
			Socket server = (Socket)ar.AsyncState;
			server.EndSend(ar);
			buff = new byte[maxSize];
			server.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), server);
		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			// server.Shutdown(SocketShutdown.Both);
			server.Close();
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			buff = Encoding.UTF8.GetBytes(tbxText.Text);
			server.BeginSend(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(SendCallback), server);
		}

		private void ShowText(string text)
		{
			tbxReceive.Text = tbxReceive.Text + text + "\r\n";
		}
	}
}
