namespace Bai3
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.tbx_domain = new System.Windows.Forms.TextBox();
			this.btn_resolve = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(38, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Domain";
			// 
			// tbx_domain
			// 
			this.tbx_domain.Location = new System.Drawing.Point(87, 24);
			this.tbx_domain.Name = "tbx_domain";
			this.tbx_domain.Size = new System.Drawing.Size(176, 20);
			this.tbx_domain.TabIndex = 1;
			// 
			// btn_resolve
			// 
			this.btn_resolve.Location = new System.Drawing.Point(269, 21);
			this.btn_resolve.Name = "btn_resolve";
			this.btn_resolve.Size = new System.Drawing.Size(75, 23);
			this.btn_resolve.TabIndex = 2;
			this.btn_resolve.Text = "Resolve";
			this.btn_resolve.UseVisualStyleBackColor = true;
			this.btn_resolve.Click += new System.EventHandler(this.btn_resolve_Click);
			// 
			// Form1
			// 
			this.AcceptButton = this.btn_resolve;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 75);
			this.Controls.Add(this.btn_resolve);
			this.Controls.Add(this.tbx_domain);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Phân giải tên miền";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbx_domain;
		private System.Windows.Forms.Button btn_resolve;
	}
}

