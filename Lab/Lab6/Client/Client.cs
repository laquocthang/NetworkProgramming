using MultiThreadLib;
using System;
using System.Net;
using System.Windows.Forms;

namespace Client
{
	public partial class Client : Form
	{
		int port;
		IPAddress ip;
		NClient client;

		public Client()
		{
			InitializeComponent();
			Form.CheckForIllegalCrossThreadCalls = false;
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			tbxStatus.Text = "Connecting ... ";
			port = GetPort();
			if (port == 0)
			{
				MessageBox.Show("The port is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			ip = GetIP();
			if (ip == IPAddress.None)
			{
				MessageBox.Show("The IP address is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			client = new NClient(ip, port);
			client.SetMessage = new NClient.SetTextToControl(SetMessage);
			client.SetStatus = new NClient.SetTextToControl(SetStatus);
			client.Connect();
		}

		private void SetMessage(string message)
		{
			tbxReceive.Text += message + "\r\n";
		}

		private void SetStatus(string message)
		{
			tbxStatus.Text = message;
		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			client.Disconnect();
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			client.Send(tbxText.Text);
			tbxText.Text = "";
		}

		private IPAddress GetIP()
		{
			IPAddress ip;
			if (IPAddress.TryParse(tbxIP.Text, out ip))
				return ip;
			return IPAddress.None;
		}

		private int GetPort()
		{
			int port;
			if (int.TryParse(tbxPort.Text, out port))
				return port;
			return 0;
		}

		private void Client_FormClosing(object sender, FormClosingEventArgs e)
		{
			btnDisconnect.PerformClick();
		}
	}
}
