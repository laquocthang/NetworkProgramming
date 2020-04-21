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
			this.rtb_Info = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(51, 33);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Domain";
			// 
			// tbx_domain
			// 
			this.tbx_domain.Location = new System.Drawing.Point(116, 30);
			this.tbx_domain.Margin = new System.Windows.Forms.Padding(4);
			this.tbx_domain.Name = "tbx_domain";
			this.tbx_domain.Size = new System.Drawing.Size(233, 22);
			this.tbx_domain.TabIndex = 1;
			// 
			// btn_resolve
			// 
			this.btn_resolve.Location = new System.Drawing.Point(359, 26);
			this.btn_resolve.Margin = new System.Windows.Forms.Padding(4);
			this.btn_resolve.Name = "btn_resolve";
			this.btn_resolve.Size = new System.Drawing.Size(100, 28);
			this.btn_resolve.TabIndex = 2;
			this.btn_resolve.Text = "Resolve";
			this.btn_resolve.UseVisualStyleBackColor = true;
			this.btn_resolve.Click += new System.EventHandler(this.btn_resolve_Click);
			// 
			// rtb_Info
			// 
			this.rtb_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtb_Info.Location = new System.Drawing.Point(16, 73);
			this.rtb_Info.Margin = new System.Windows.Forms.Padding(4);
			this.rtb_Info.Name = "rtb_Info";
			this.rtb_Info.Size = new System.Drawing.Size(475, 253);
			this.rtb_Info.TabIndex = 3;
			this.rtb_Info.Text = "";
			// 
			// Form1
			// 
			this.AcceptButton = this.btn_resolve;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(508, 342);
			this.Controls.Add(this.rtb_Info);
			this.Controls.Add(this.btn_resolve);
			this.Controls.Add(this.tbx_domain);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "Phân giải tên miền";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbx_domain;
		private System.Windows.Forms.Button btn_resolve;
		private System.Windows.Forms.RichTextBox rtb_Info;
	}
}

