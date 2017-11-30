namespace hotspot
{
    partial class window_options
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.check_notify_device = new System.Windows.Forms.CheckBox();
            this.check_notify_all = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(87, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Options";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.check_notify_device);
            this.panel1.Controls.Add(this.check_notify_all);
            this.panel1.Location = new System.Drawing.Point(33, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 102);
            this.panel1.TabIndex = 3;
            // 
            // check_notify_device
            // 
            this.check_notify_device.AutoSize = true;
            this.check_notify_device.Location = new System.Drawing.Point(34, 63);
            this.check_notify_device.Name = "check_notify_device";
            this.check_notify_device.Size = new System.Drawing.Size(159, 17);
            this.check_notify_device.TabIndex = 3;
            this.check_notify_device.Text = "Disable Device Notifications";
            this.check_notify_device.UseVisualStyleBackColor = true;
            // 
            // check_notify_all
            // 
            this.check_notify_all.AutoSize = true;
            this.check_notify_all.Location = new System.Drawing.Point(34, 24);
            this.check_notify_all.Name = "check_notify_all";
            this.check_notify_all.Size = new System.Drawing.Size(130, 17);
            this.check_notify_all.TabIndex = 2;
            this.check_notify_all.Text = "Disable All Notifiations";
            this.check_notify_all.UseVisualStyleBackColor = true;
            this.check_notify_all.CheckedChanged += new System.EventHandler(this.check_notify_all_CheckedChanged);
            // 
            // window_options
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 221);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "window_options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.window_options_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox check_notify_device;
        private System.Windows.Forms.CheckBox check_notify_all;
    }
}