﻿namespace dnscrypt_winclient
{
	partial class ApplicationForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationForm));
            this.DNSlistbox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.service_label = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyIconContextOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconContextExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hideAdaptersCheckbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.protoTCP = new System.Windows.Forms.RadioButton();
            this.protoUDP = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.ComboBox();
            this.serverGroupBox = new System.Windows.Forms.GroupBox();
            this.serverBox = new System.Windows.Forms.ComboBox();
            this.gatewayCheckbox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.parentalControlsRadio = new System.Windows.Forms.RadioButton();
            this.service_button = new System.Windows.Forms.Button();
            this.ipv6Radio = new System.Windows.Forms.RadioButton();
            this.ipv4Radio = new System.Windows.Forms.RadioButton();
            this.notifyIconContextMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.serverGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DNSlistbox
            // 
            this.DNSlistbox.BackColor = System.Drawing.Color.White;
            this.DNSlistbox.CheckOnClick = true;
            this.DNSlistbox.ForeColor = System.Drawing.Color.Black;
            this.DNSlistbox.FormattingEnabled = true;
            this.DNSlistbox.HorizontalScrollbar = true;
            this.DNSlistbox.Location = new System.Drawing.Point(7, 25);
            this.DNSlistbox.Name = "DNSlistbox";
            this.DNSlistbox.Size = new System.Drawing.Size(521, 109);
            this.DNSlistbox.TabIndex = 0;
            this.DNSlistbox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DNSlisbox_itemcheck_statechanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select network devices for use with DNSCrypt";
            // 
            // service_label
            // 
            this.service_label.BackColor = System.Drawing.Color.DarkOrange;
            this.service_label.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.service_label.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.service_label.ForeColor = System.Drawing.Color.White;
            this.service_label.Location = new System.Drawing.Point(0, 401);
            this.service_label.Name = "service_label";
            this.service_label.Size = new System.Drawing.Size(531, 21);
            this.service_label.TabIndex = 2;
            this.service_label.Text = "DNSCrypt is NOT running";
            this.service_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "DNSCrypt Proxy Client";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyIconContextMenu
            // 
            this.notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyIconContextOpen,
            this.notifyIconContextExit});
            this.notifyIconContextMenu.Name = "notifyIconContextMenu";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(104, 48);
            // 
            // notifyIconContextOpen
            // 
            this.notifyIconContextOpen.Name = "notifyIconContextOpen";
            this.notifyIconContextOpen.Size = new System.Drawing.Size(103, 22);
            this.notifyIconContextOpen.Text = "Open";
            this.notifyIconContextOpen.Click += new System.EventHandler(this.notifyIcon_Open);
            // 
            // notifyIconContextExit
            // 
            this.notifyIconContextExit.Name = "notifyIconContextExit";
            this.notifyIconContextExit.Size = new System.Drawing.Size(103, 22);
            this.notifyIconContextExit.Text = "Exit";
            this.notifyIconContextExit.Click += new System.EventHandler(this.notifyIcon_Exit);
            // 
            // tmr
            // 
            this.tmr.Interval = 50;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 268);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(522, 130);
            this.textBox1.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.hideAdaptersCheckbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.protoTCP);
            this.groupBox1.Controls.Add(this.protoUDP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.portBox);
            this.groupBox1.Controls.Add(this.serverGroupBox);
            this.groupBox1.Location = new System.Drawing.Point(6, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 122);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // hideAdaptersCheckbox
            // 
            this.hideAdaptersCheckbox.AutoSize = true;
            this.hideAdaptersCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hideAdaptersCheckbox.Location = new System.Drawing.Point(377, 21);
            this.hideAdaptersCheckbox.Name = "hideAdaptersCheckbox";
            this.hideAdaptersCheckbox.Size = new System.Drawing.Size(132, 17);
            this.hideAdaptersCheckbox.TabIndex = 19;
            this.hideAdaptersCheckbox.Text = "Show hidden adapters";
            this.hideAdaptersCheckbox.UseVisualStyleBackColor = true;
            this.hideAdaptersCheckbox.CheckedChanged += new System.EventHandler(this.refreshNICList);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Protocol:";
            // 
            // protoTCP
            // 
            this.protoTCP.AutoSize = true;
            this.protoTCP.Location = new System.Drawing.Point(116, 20);
            this.protoTCP.Name = "protoTCP";
            this.protoTCP.Size = new System.Drawing.Size(46, 17);
            this.protoTCP.TabIndex = 13;
            this.protoTCP.Text = "TCP";
            this.protoTCP.UseVisualStyleBackColor = true;
            // 
            // protoUDP
            // 
            this.protoUDP.AutoSize = true;
            this.protoUDP.Checked = true;
            this.protoUDP.Location = new System.Drawing.Point(65, 20);
            this.protoUDP.Name = "protoUDP";
            this.protoUDP.Size = new System.Drawing.Size(48, 17);
            this.protoUDP.TabIndex = 12;
            this.protoUDP.TabStop = true;
            this.protoUDP.Text = "UDP";
            this.protoUDP.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Port:";
            // 
            // portBox
            // 
            this.portBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portBox.FormattingEnabled = true;
            this.portBox.Items.AddRange(new object[] {
            "53",
            "443",
            "5353"});
            this.portBox.Location = new System.Drawing.Point(203, 19);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(62, 21);
            this.portBox.TabIndex = 11;
            // 
            // serverGroupBox
            // 
            this.serverGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.serverGroupBox.Controls.Add(this.serverBox);
            this.serverGroupBox.Controls.Add(this.gatewayCheckbox);
            this.serverGroupBox.Controls.Add(this.label4);
            this.serverGroupBox.Controls.Add(this.parentalControlsRadio);
            this.serverGroupBox.Controls.Add(this.service_button);
            this.serverGroupBox.Controls.Add(this.ipv6Radio);
            this.serverGroupBox.Controls.Add(this.ipv4Radio);
            this.serverGroupBox.Location = new System.Drawing.Point(6, 46);
            this.serverGroupBox.Name = "serverGroupBox";
            this.serverGroupBox.Size = new System.Drawing.Size(509, 70);
            this.serverGroupBox.TabIndex = 18;
            this.serverGroupBox.TabStop = false;
            this.serverGroupBox.Text = "Server Connection";
            // 
            // serverBox
            // 
            this.serverBox.DisplayMember = "1";
            this.serverBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serverBox.FormattingEnabled = true;
            this.serverBox.Items.AddRange(new object[] {
            "1.OpenDNS               [USA]",
            "2.CloudNS.com.au    [Canberra, Australia]",
            "3.CloudNS.com.au    [Sydney, Australia]",
            "4.OpenNIC                [Japan]",
            "5.DNSCrypt.eu          [Holland]"});
            this.serverBox.Location = new System.Drawing.Point(287, 45);
            this.serverBox.Name = "serverBox";
            this.serverBox.Size = new System.Drawing.Size(216, 21);
            this.serverBox.TabIndex = 18;
            this.serverBox.SelectedIndexChanged += new System.EventHandler(this.serverBox_SelectedIndexChanged);
            // 
            // gatewayCheckbox
            // 
            this.gatewayCheckbox.AutoSize = true;
            this.gatewayCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gatewayCheckbox.Location = new System.Drawing.Point(360, 19);
            this.gatewayCheckbox.Name = "gatewayCheckbox";
            this.gatewayCheckbox.Size = new System.Drawing.Size(143, 17);
            this.gatewayCheckbox.TabIndex = 21;
            this.gatewayCheckbox.Text = "Act as a gateway device";
            this.gatewayCheckbox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Server:";
            // 
            // parentalControlsRadio
            // 
            this.parentalControlsRadio.AutoSize = true;
            this.parentalControlsRadio.Location = new System.Drawing.Point(113, 19);
            this.parentalControlsRadio.Name = "parentalControlsRadio";
            this.parentalControlsRadio.Size = new System.Drawing.Size(139, 17);
            this.parentalControlsRadio.TabIndex = 2;
            this.parentalControlsRadio.Text = "Enable parental controls";
            this.parentalControlsRadio.UseVisualStyleBackColor = true;
            // 
            // service_button
            // 
            this.service_button.Location = new System.Drawing.Point(6, 43);
            this.service_button.Name = "service_button";
            this.service_button.Size = new System.Drawing.Size(75, 23);
            this.service_button.TabIndex = 3;
            this.service_button.Text = "Start";
            this.service_button.UseVisualStyleBackColor = true;
            this.service_button.Click += new System.EventHandler(this.service_button_Click);
            // 
            // ipv6Radio
            // 
            this.ipv6Radio.AutoSize = true;
            this.ipv6Radio.Location = new System.Drawing.Point(59, 19);
            this.ipv6Radio.Name = "ipv6Radio";
            this.ipv6Radio.Size = new System.Drawing.Size(47, 17);
            this.ipv6Radio.TabIndex = 1;
            this.ipv6Radio.Text = "IPv6";
            this.ipv6Radio.UseVisualStyleBackColor = true;
            // 
            // ipv4Radio
            // 
            this.ipv4Radio.AutoSize = true;
            this.ipv4Radio.Checked = true;
            this.ipv4Radio.Location = new System.Drawing.Point(6, 19);
            this.ipv4Radio.Name = "ipv4Radio";
            this.ipv4Radio.Size = new System.Drawing.Size(47, 17);
            this.ipv4Radio.TabIndex = 0;
            this.ipv4Radio.TabStop = true;
            this.ipv4Radio.Text = "IPv4";
            this.ipv4Radio.UseVisualStyleBackColor = true;
            // 
            // ApplicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 422);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DNSlistbox);
            this.Controls.Add(this.service_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DNSCrypt Proxy Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_closing);
            this.Load += new System.EventHandler(this.ApplicationForm_Load);
            this.Resize += new System.EventHandler(this.form_resized);
            this.notifyIconContextMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.serverGroupBox.ResumeLayout(false);
            this.serverGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox DNSlistbox;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label service_label;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip notifyIconContextMenu;
		private System.Windows.Forms.ToolStripMenuItem notifyIconContextOpen;
        private System.Windows.Forms.ToolStripMenuItem notifyIconContextExit;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox serverGroupBox;
        private System.Windows.Forms.CheckBox hideAdaptersCheckbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton protoTCP;
        private System.Windows.Forms.RadioButton protoUDP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox portBox;
        private System.Windows.Forms.CheckBox gatewayCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton parentalControlsRadio;
        private System.Windows.Forms.Button service_button;
        private System.Windows.Forms.RadioButton ipv6Radio;
        private System.Windows.Forms.RadioButton ipv4Radio;
        private System.Windows.Forms.ComboBox serverBox;
	}
}

