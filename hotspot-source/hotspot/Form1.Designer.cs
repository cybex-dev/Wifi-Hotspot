namespace hotspot
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.input_mode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.input_key = new System.Windows.Forms.TextBox();
            this.input_ssid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this._contextmenu_main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startStopHotspotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.input_hotspot_subnet = new System.Windows.Forms.TextBox();
            this.input_hotspot_ip = new System.Windows.Forms.TextBox();
            this.check_autoconfig = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.display_connected_Devices = new System.Windows.Forms.ListView();
            this._col_IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_MAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_PING = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_AvePing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._col_LOSS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.action_start_stop = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this._contextmenu_main.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.input_mode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.input_key);
            this.panel1.Controls.Add(this.input_ssid);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(11, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 186);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(50, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Quick Settings";
            // 
            // input_mode
            // 
            this.input_mode.FormattingEnabled = true;
            this.input_mode.Items.AddRange(new object[] {
            "Allow",
            "Deny"});
            this.input_mode.Location = new System.Drawing.Point(94, 130);
            this.input_mode.Name = "input_mode";
            this.input_mode.Size = new System.Drawing.Size(100, 21);
            this.input_mode.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mode";
            // 
            // input_key
            // 
            this.input_key.Location = new System.Drawing.Point(94, 92);
            this.input_key.Name = "input_key";
            this.input_key.Size = new System.Drawing.Size(100, 20);
            this.input_key.TabIndex = 3;
            // 
            // input_ssid
            // 
            this.input_ssid.Location = new System.Drawing.Point(94, 52);
            this.input_ssid.Name = "input_ssid";
            this.input_ssid.Size = new System.Drawing.Size(100, 20);
            this.input_ssid.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hotspot Name";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "A Balloon tip text for useful info";
            this.notifyIcon1.BalloonTipTitle = "A Balloon Title";
            this.notifyIcon1.ContextMenuStrip = this._contextmenu_main;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // _contextmenu_main
            // 
            this._contextmenu_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideToolStripMenuItem,
            this.startStopHotspotToolStripMenuItem,
            this.exitToolStripMenuItem});
            this._contextmenu_main.Name = "_contextmenu_main";
            this._contextmenu_main.Size = new System.Drawing.Size(174, 70);
            this._contextmenu_main.Text = "contextmenu text";
            this._contextmenu_main.Opening += new System.ComponentModel.CancelEventHandler(this._contextmenu_main_Opening);
            // 
            // showHideToolStripMenuItem
            // 
            this.showHideToolStripMenuItem.Name = "showHideToolStripMenuItem";
            this.showHideToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showHideToolStripMenuItem.Text = "Show/Hide";
            this.showHideToolStripMenuItem.Click += new System.EventHandler(this.showHideToolStripMenuItem_Click);
            // 
            // startStopHotspotToolStripMenuItem
            // 
            this.startStopHotspotToolStripMenuItem.Name = "startStopHotspotToolStripMenuItem";
            this.startStopHotspotToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.startStopHotspotToolStripMenuItem.Text = "Start/Stop Hotspot";
            this.startStopHotspotToolStripMenuItem.Click += new System.EventHandler(this.startStopHotspotToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.input_hotspot_subnet);
            this.panel3.Controls.Add(this.input_hotspot_ip);
            this.panel3.Controls.Add(this.check_autoconfig);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(233, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(216, 186);
            this.panel3.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(75, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "set props";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(71, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 6;
            this.label8.Text = "Advanced";
            // 
            // input_hotspot_subnet
            // 
            this.input_hotspot_subnet.Location = new System.Drawing.Point(91, 131);
            this.input_hotspot_subnet.Name = "input_hotspot_subnet";
            this.input_hotspot_subnet.Size = new System.Drawing.Size(100, 20);
            this.input_hotspot_subnet.TabIndex = 5;
            // 
            // input_hotspot_ip
            // 
            this.input_hotspot_ip.Location = new System.Drawing.Point(91, 93);
            this.input_hotspot_ip.Name = "input_hotspot_ip";
            this.input_hotspot_ip.Size = new System.Drawing.Size(100, 20);
            this.input_hotspot_ip.TabIndex = 4;
            // 
            // check_autoconfig
            // 
            this.check_autoconfig.AutoSize = true;
            this.check_autoconfig.Checked = true;
            this.check_autoconfig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_autoconfig.Location = new System.Drawing.Point(18, 55);
            this.check_autoconfig.Name = "check_autoconfig";
            this.check_autoconfig.Size = new System.Drawing.Size(171, 17);
            this.check_autoconfig.TabIndex = 3;
            this.check_autoconfig.Text = "Auto-Config Network Settings?";
            this.check_autoconfig.UseVisualStyleBackColor = true;
            this.check_autoconfig.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Subnet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "IP of hotspot";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.display_connected_Devices);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(11, 269);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(438, 148);
            this.panel4.TabIndex = 3;
            // 
            // display_connected_Devices
            // 
            this.display_connected_Devices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._col_IP,
            this._col_MAC,
            this._col_PING,
            this._col_AvePing,
            this._col_LOSS});
            this.display_connected_Devices.FullRowSelect = true;
            this.display_connected_Devices.Location = new System.Drawing.Point(10, 32);
            this.display_connected_Devices.MultiSelect = false;
            this.display_connected_Devices.Name = "display_connected_Devices";
            this.display_connected_Devices.Size = new System.Drawing.Size(413, 105);
            this.display_connected_Devices.TabIndex = 6;
            this.display_connected_Devices.UseCompatibleStateImageBehavior = false;
            this.display_connected_Devices.View = System.Windows.Forms.View.Details;
            // 
            // _col_IP
            // 
            this._col_IP.Text = "IP Address";
            this._col_IP.Width = 121;
            // 
            // _col_MAC
            // 
            this._col_MAC.Text = "Physical Address";
            this._col_MAC.Width = 136;
            // 
            // _col_PING
            // 
            this._col_PING.Text = "Ping";
            this._col_PING.Width = 35;
            // 
            // _col_AvePing
            // 
            this._col_AvePing.Text = "Average";
            // 
            // _col_LOSS
            // 
            this._col_LOSS.Text = "Loss";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(136, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Connected Devices";
            // 
            // action_start_stop
            // 
            this.action_start_stop.BackColor = System.Drawing.SystemColors.Control;
            this.action_start_stop.Location = new System.Drawing.Point(157, 423);
            this.action_start_stop.Name = "action_start_stop";
            this.action_start_stop.Size = new System.Drawing.Size(146, 60);
            this.action_start_stop.TabIndex = 4;
            this.action_start_stop.Text = "Start Hotspot";
            this.action_start_stop.UseVisualStyleBackColor = false;
            this.action_start_stop.Click += new System.EventHandler(this.action_start_stop_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(927, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click_1);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(11, 221);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(438, 42);
            this.panel2.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(107, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(314, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "You must manually assign an IP to your device when connecting!";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(10, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "PLEASE NOTE: ";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(513, 46);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(388, 412);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(596, 459);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "disable";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(703, 459);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "enable";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 517);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.action_start_stop);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._contextmenu_main.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox input_key;
        private System.Windows.Forms.TextBox input_ssid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ComboBox input_mode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox input_hotspot_ip;
        private System.Windows.Forms.CheckBox check_autoconfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox input_hotspot_subnet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button action_start_stop;
        private System.Windows.Forms.ListView display_connected_Devices;
        private System.Windows.Forms.ColumnHeader _col_PING;
        private System.Windows.Forms.ColumnHeader _col_IP;
        private System.Windows.Forms.ColumnHeader _col_MAC;
        private System.Windows.Forms.ContextMenuStrip _contextmenu_main;
        private System.Windows.Forms.ToolStripMenuItem showHideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startStopHotspotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader _col_AvePing;
        private System.Windows.Forms.ColumnHeader _col_LOSS;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

