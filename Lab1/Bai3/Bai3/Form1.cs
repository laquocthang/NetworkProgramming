using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		static void getHostInfo(string host)
		{
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(host);
				Entry entry = new Entry(hostEntry.AddressList, hostEntry.HostName);
				MessageBox.Show(entry.ToString(), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception)
			{
				MessageBox.Show("Can't resolve this domain", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btn_resolve_Click(object sender, EventArgs e)
		{
			getHostInfo(tbx_domain.Text);
		}
	}
}
