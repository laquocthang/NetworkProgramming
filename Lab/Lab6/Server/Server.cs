using MultiThreadLib;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab6
{
	public partial class Server : Form
	{
		private NServer server;

		public Server()
		{
			InitializeComponent();
			Form.CheckForIllegalCrossThreadCalls = false;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			int port = GetPort();
			if (port == 0)
			{
				MessageBox.Show("The port is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			server = new NServer(IPAddress.Any, port);
			server.SetMessage = new NServer.SetTextToControl(SetMessage);
			server.SetStatus = new NServer.SetTextToControl(SetStatus);
			server.Start();
		}

		private void SetStatus(string message)
		{
			tbxStatus.Text = message;
		}

		private void SetMessage(string message)
		{
			tbxReceive.Text += message + "\r\n";
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			server.Stop();
		}

		private void Server_FormClosing(object sender, FormClosingEventArgs e)
		{
			btnStop.PerformClick();
		}

		private int GetPort()
		{
			int port;
			if (int.TryParse(tbxPort.Text, out port))
				return port;
			return 0;
		}
	}
}
