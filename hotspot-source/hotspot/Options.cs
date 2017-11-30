using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotspot
{
    public partial class window_options : Form
    {
        int _check_disable = 0; //0 - none, 1 - device notify, 2 - all
        public window_options(int _bOptions)
        {
            InitializeComponent();
            set_Options(_bOptions);
        }

        private void set_Options(int _bOptions)
        {
            switch (_bOptions)
            {
                case 1: check_notify_device.Checked = true;
                    break;
                case 2:
                    check_notify_device.Enabled = false;
                    check_notify_all.Checked = true;
                    break;
            }
        }

        private void check_notify_all_CheckedChanged(object sender, EventArgs e)
        {
            check_notify_device.Enabled = (check_notify_all.Checked) ? false : true;
        }

        private void window_options_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (check_notify_all.Checked)
            {
                _check_disable = 2;
            }
            else
            {
                if (check_notify_device.Checked)
                {
                    _check_disable = 1;
                }
                else _check_disable = 0;
            }
            Form1._bOptions_Check_Notify = _check_disable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
