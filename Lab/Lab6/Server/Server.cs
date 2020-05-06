using MultiThreadLib;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Lab6
{
	public partial class Server : Form
	{
		private int port;
		private Socket serverSocket;
		private IPEndPoint serverEndPoint;
		private NServer server;

		public Server()
		{
			InitializeComponent();
			Form.CheckForIllegalCrossThreadCalls = false;
		}

		private void DoOnRealTime()
		{
			while (true)
			{
				new Thread(server.Start).Start();
			}
		}

		private void Init()
		{
			serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			serverEndPoint = new IPEndPoint(IPAddress.Any, port);
			serverSocket.Bind(serverEndPoint);
			serverSocket.Listen(5);

			server = new NServer(IPAddress.Any, port, serverSocket);
			server.SetMessage = new NServer.SetTextToControl(SetMessage);
			server.SetStatus = new NServer.SetTextToControl(SetStatus);
			server.SetClient = new NServer.SetTextToControl(SetClient);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			port = GetPort();
			if (port == 0)
			{
				MessageBox.Show("The port is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Init();
			new Thread(DoOnRealTime).Start();
		}

		private void SetClient(string message)
		{
			cbxClients.Items.Add(message);
			cbxClients.SelectedIndex = 0;
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
