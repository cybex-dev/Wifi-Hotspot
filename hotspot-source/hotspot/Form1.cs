using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using Microsoft.Win32;
using System.Net.Sockets;


namespace hotspot
{
    public partial class Form1 : Form
    {
        static bool _bHotspot_Running = false;                         //flag for start/stop hotspt
        static string _log_filename;                                   //log file name
        static StreamWriter _logOut;                                   //logfile streamwriter
        static Thread _loop_main;                                       //thread executed after hotspot is started, running :  void loop()
        static bool _bFormVisible;
        static bool _bExit = false;
        static List<device> _ipcol_all_devices = new List<device>();
        static List<device> _devices_new = new List<device>();
        static List<device> _devices_dc = new List<device>();
        static public int _bOptions_Check_Notify = 0;

        string _ssid, _passphrase, _mode, _hotspot_ip, _hotspot_subnet, // user input options for hotspot
            _ip_local_vwifi_network;                                    //virtual wifi network i.e. first 3 combinations in _hotspot_ip
        const string _autoconfig_ip_hotspot = "192.168.0.1",
            _autoconfig_subnet_hotspot = "255.255.255.0";               //fallback autoconfig for hotspot
        NetworkInterface _nic_vwifi;                                    //interface of virtual interface

        nic_adap nic_adapt;

        System.Windows.Forms.Timer singleClickTimer = new System.Windows.Forms.Timer();
        //TImer for single click double click issue - notification icon

        public Form1()
        {
            InitializeComponent();
            initialize_Log();
            gui_startup();
            customcommands();
        }

        private void customcommands()
        {
            input_ssid.Text = "hotspot";
            input_key.Text = "12345678";
            input_hotspot_ip.Text = "192.168.0.1";
            input_hotspot_subnet.Text = "255.255.255.0";
        }

        private void start_Hotspot()
        {
            _ssid = input_ssid.Text;
            _passphrase = input_key.Text;
            _mode = input_mode.Text;
            _hotspot_ip = input_hotspot_ip.Text;
            _hotspot_subnet = input_hotspot_subnet.Text;

            string _netsh_arg = "netsh wlan set hostednetwork ssid=" + _ssid + " key=" + _passphrase + " mode=" + _mode + " & netsh wlan start hostednetwork",

             _assignip = "netsh interface ip set address \"Local Area Connection * 12\" static " + _hotspot_ip + " " + _hotspot_subnet + " " + _hotspot_ip;

            Process _netsh_hotspot_start = create_Process(_netsh_arg);
            launch_process(_netsh_hotspot_start);


            string _hotspot_error = _netsh_hotspot_start.StandardError.ReadToEnd(),
            _hotspot_output = _netsh_hotspot_start.StandardOutput.ReadToEnd();
            write_to_log("\nHOTSPOT START\nERROR:\n" + _hotspot_error + "\nOUTPUT:\n" + _hotspot_output + "\n");
            if (_hotspot_error == "" && new Regex("The hosted network started").IsMatch(_hotspot_output))
            {
                action_start_stop.BackColor = Color.Green;
                preloop();
            }
            else
            {
                action_start_stop.BackColor = Color.Red;
                MessageBox.Show("Error starting hotspot, falling back to other methods, see log for details");
            }
        }

        private void stop_Hotspot()
        {
            string _netsh_arg_stop = "netsh wlan stop hostednetwork";
            Process _netsh_hotspot_stop = create_Process(_netsh_arg_stop);
            launch_process(_netsh_hotspot_stop);

            string _hotspot_error = _netsh_hotspot_stop.StandardError.ReadToEnd();
            string _hotspot_output = _netsh_hotspot_stop.StandardOutput.ReadToEnd();

            write_to_log("\nHOTSPOT STOP\nERROR:\n" + _hotspot_error + "\nOUTPUT:\n" + _hotspot_output + "\n");

            if (_hotspot_error == "" || new Regex("The hosted network stopped").IsMatch(_hotspot_output))
            {
                action_start_stop.BackColor = Color.LightGreen;
            }
            else
            {
                action_start_stop.BackColor = Color.OrangeRed;
                MessageBox.Show("Error stoping hotspot, see log for details");
            }
        }

        private void preloop()
        {
            //one time run in loop
            //should check for new users connected

            //update_Connected_Devices();

            adapterIPandProp();

            _loop_main = new Thread(new ThreadStart(loop));
            _loop_main.Start();

            //display user info
        }

        private void loop()
        {
            while (_bHotspot_Running)
            {
                update_Connected_Devices();
                update_GUI();
                Thread.Sleep(1000);
            }
        }

        //-----------------SECONDARY METHODS-----------------------

        private bool compat_check()
        {
            Regex _hostednetworkcheck = new Regex("Hosted network supported  : Yes");
            string _netsh_show_drivers = "netsh wlan show drivers";
            string _drivers_output, _drivers_error;

            Process _netsh_compat = create_Process(_netsh_show_drivers);
            launch_process(_netsh_compat);

            _drivers_error = _netsh_compat.StandardError.ReadToEnd();
            _drivers_output = _netsh_compat.StandardOutput.ReadToEnd();
            write_to_log("\nCOMPAT CHECK START\nERROR:\n" + _drivers_error + "\nOUTPUT:\n" + _drivers_output + "\n");
            return (_hostednetworkcheck.IsMatch(_drivers_output)) ? true : false;

        }

        private bool check_Valid_Fields()
        {
            bool _bBox1 = true, _bBox2 = true, _bBox2l = true, _bBox1l = true;
            if (!checkString(input_ssid.Text))
            {
                input_ssid.BackColor = Color.Red;
                _bBox1 = false;
            }
            if (!checkString(input_key.Text))
            {
                input_key.BackColor = Color.Red;
                _bBox2 = false;
            }
            if (input_ssid.Text.Length == 0)
                _bBox1l = false;
            if (input_key.Text.Length < 8)
                _bBox2l = false;
            return (_bBox1 && _bBox2 && _bBox2l && _bBox1l) ? true : false;
        }

        void update_Connected_Devices()
        {
            _ipcol_all_devices = get_All_Connected_Devices(_nic_vwifi);
            check_new_devices(_ipcol_all_devices);
            check_dc_devices(_ipcol_all_devices);
            _devices_new = _ipcol_all_devices;
            _devices_dc = _ipcol_all_devices;
            connected_clear_text();
            foreach (device item in _ipcol_all_devices)
            {
                connected_add_device(item, true);
            }
        }

        void get_IP_Host_Info(IPAddress IP)
        {
            IPHostEntry _hostname = Dns.GetHostEntry(IP);
        }

        private List<device> get_All_Connected_Devices(NetworkInterface _nic_vwifi)
        {
            string _arp_string = "arp -a | findstr -i " + _ip_local_vwifi_network + " | findstr /V 255 | findstr /V " + _hotspot_ip;
            Process _arp_get_all_device_ip = create_Process(_arp_string);
            launch_process(_arp_get_all_device_ip);

            List<string> _arp_raw_data = new List<string>();
            while (!_arp_get_all_device_ip.StandardOutput.EndOfStream)
            {
                _arp_raw_data.Add(_arp_get_all_device_ip.StandardOutput.ReadLine());
            }

            List<device> _devices_alive = process_arp_output(_arp_raw_data);
            return _devices_alive;
        }

        private void check_dc_devices(List<device> _ipcol_all_devices)
        {
            bool bCheckDC = false;
            foreach (device _current_dc in _devices_dc)
            {
                bCheckDC = false;
                foreach (device _current_alive in _ipcol_all_devices)
                {
                    if (_current_dc.Equals(_current_alive)) //remove since devices is reconnected
                    {
                        bCheckDC = true;
                        break;
                    }

                }
                if (!bCheckDC) notifyicon_device_dc(_current_dc);
            }
        }

        private void check_new_devices(List<device> _devices_alive)
        {
            bool _bCheckNew = false;
            foreach (device _current_new in _devices_alive)
            {
                _bCheckNew = false;
                foreach (device _current_alive in _devices_new)
                {
                    if (_current_new.Equals(_current_alive))
                    {
                        _bCheckNew = true;
                        break;
                    }
                }
                if (!_bCheckNew) notifyicon_device_new(_current_new);
            }
        }

        //-----------------UTILITY METHODS--------------------------

        public int Options_Notify_Code
        {
            get { return _bOptions_Check_Notify; }
            set { _bOptions_Check_Notify = value; }
        }

        private Process create_Process(string _arg)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Windows\\System32\\cmd.exe",
                    Arguments = "/c " + _arg,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    Verb = "runas",
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
        }

        private void launch_process(Process _proc_Start)
        {
            _proc_Start.Start();
        }

        private bool checkString(string text)
        {
            Regex _specialchar = new Regex("/|\"'?<>!@#$%^&*() ");
            bool _bChar = _specialchar.IsMatch(text);
            if (text == "")
            {
                MessageBox.Show("Field is blank");
                return false;
            }
            if (!_bChar)
                return true;
            else
            {
                MessageBox.Show("Contains special characters i.e. it is not a normal string");
                return false;
            }
        }

        private List<device> process_arp_output(List<string> _arp_raw_data)
        {
            List<device> _connected_devices = new List<device>();
            string temp = "";
            string[] x = null;

            foreach (string item in _arp_raw_data)
            {
                temp = new Regex(@"\s+").Replace(item, " ");
                x = temp.Split(' ');
                _connected_devices.Add(new device(IPAddress.Parse(x.ElementAt(1)), x.ElementAt(2)));
            }

            return check_Alive_devices(_connected_devices);
        }

        private List<device> check_Alive_devices(List<device> _clients)
        {
            List<device> _alive = new List<device>();
            _clients = compareClientList(_clients); //check for cliets already connected for ping data to be consistent
            foreach (device item in _clients)
            {

                PingReply _client_reply = new Ping().Send(item._deviceIP, 1000, new byte[1]);
                item._devicePing = _client_reply;
                IPStatus _client_status = _client_reply.Status;
                switch (_client_status)
                {
                    case IPStatus.TimedOut:
                        {
                            item.deviceStatus = Color.Red;
                            item._count_ping_timeout++;
                        }
                        break;

                    case IPStatus.Success:
                        {
                            item._sum_ping_time = item._sum_ping_time + (int)_client_reply.RoundtripTime;
                            item.deviceStatus = Color.Green;
                            if (_client_reply.RoundtripTime >= 50)
                            {
                                item.deviceStatus = Color.LightGreen;
                            }
                            if (_client_reply.RoundtripTime >= 100)
                            {
                                item.deviceStatus = Color.Yellow;
                            }
                            if (_client_reply.RoundtripTime >= 200)
                            {
                                item.deviceStatus = Color.Orange;
                            }
                            if (_client_reply.RoundtripTime >= 300)
                            {
                                item.deviceStatus = Color.OrangeRed;
                            }

                            _alive.Add(item);
                            break;
                        }

                    default:
                        {
                            item.deviceStatus = Color.Blue;
                            write_to_log("Ping Reply Status Blue with status message - " + _client_status);
                        }
                        break;
                }
                item._count_ping++;
            }
            return _alive;
        }

        private List<device> compareClientList(List<device> _clients)
        {
            List<device> _new_cur_clients = new List<device>();
            bool _bCurClient = false;
            foreach (device _new_client in _clients)
            {
                _bCurClient = false;
                foreach (device _cur_client in _ipcol_all_devices)
                {
                    if (_new_client.Equals(_cur_client))
                    {
                        _new_cur_clients.Add(_cur_client);
                        _bCurClient = true;
                        break;
                    }
                }
                if (!_bCurClient) _new_cur_clients.Add(_new_client);
            }
            return _new_cur_clients;
        }

        void adapterIPandProp()
        {
            string _ip_vwifi_adapter = input_hotspot_ip.Text;
            _ip_local_vwifi_network = _ip_vwifi_adapter.Substring(0, _ip_vwifi_adapter.LastIndexOf('.'));

            NetworkInterface[] t = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                richTextBox1.AppendText("=======================\n\nAdapter = " + item.Name + "\nDescription = " + item.Description + "\nID = " + item.Id + "\n\n=======================");
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && _ip_vwifi_adapter == ip.Address.ToString())
                    {
                        _nic_vwifi = item;
                    }
                }
            }
        }

        private void autoconfig_change(bool v)
        {
            if (v)
            {
                input_hotspot_ip.Enabled = false;
                input_hotspot_subnet.Enabled = false;
                _hotspot_ip = _autoconfig_ip_hotspot;
                _hotspot_subnet = _autoconfig_subnet_hotspot;
            }
            else
            {
                input_hotspot_ip.Enabled = true;
                input_hotspot_subnet.Enabled = true;
            }
        }

        //-----------------GUI METHODS--------------------------

        private void gui_startup()
        {
            this.Text = "WiFi Hotspot";
            if (compat_check())
            {
                action_start_stop.BackColor = Color.LightGreen;
                input_mode.SelectedIndex = 0;
                autoconfig_change(true);
            }
            else
            {
                MessageBox.Show("Your WiFi adapter does not support creating a virtual WiFi network");
                action_start_stop.BackColor = Color.Orange;
                action_start_stop.Enabled = false;
            }
        }

        private void update_GUI()
        {
            form_set_text("WiFi Hotspot - " + _ipcol_all_devices.Count + " Devices connected");
        }

        private void action_start_stop_Click(object sender, EventArgs e)
        {
            action_start_stop.BackColor = Color.LightGreen;
            input_ssid.BackColor = SystemColors.Control;
            input_key.BackColor = SystemColors.Control;

            if (!_bHotspot_Running)
            {
                if (check_Valid_Fields())
                {
                    action_start_stop.Text = "Stop Hotspot";
                    _bHotspot_Running = true;
                    start_Hotspot();
                    notifyicon_hotspot_on();
                }
            }
            else
            {
                action_start_stop.Text = "Start Hotspot";
                _bHotspot_Running = false;
                stop_Hotspot();
                notifyicon_hotspot_off();
            }
        }

        //private void richTextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    //richTextBox1.SelectionStart = richTextBox1.Text.Length;
        //    //richTextBox1.ScrollToCaret();
        //}

        void connected_clear_text()
        {
            if (display_connected_Devices.InvokeRequired)
                display_connected_Devices.BeginInvoke((MethodInvoker)delegate ()
                {
                    display_connected_Devices.Items.Clear();
                }
                );
            else
            {
                display_connected_Devices.Items.Clear();
            }
        }

        void connected_add_device(device _client, bool _bAdd_Color)
        {
            string[] _listitem_builder = { _client._deviceIP.ToString(), _client._deviceMAC, _client.get_Device_Ping().ToString(), _client.get_average_ping().ToString(), _client.get_loss().ToString() };
            ListViewItem _item_device = new ListViewItem(_listitem_builder);
            if (_bAdd_Color)
                _item_device.BackColor = _client._deviceStatus;

            if (display_connected_Devices.InvokeRequired)
                display_connected_Devices.BeginInvoke((MethodInvoker)delegate ()
                {
                    display_connected_Devices.Items.Add(_item_device);
                }
                );
            else
                display_connected_Devices.Items.Add(_item_device);
        }

        void form_set_text(string title)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.Text = title;
                }
                );
            else
            {
                this.Text = title;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_bExit)
            {
                e.Cancel = true;
                _bFormVisible = false;
                TrayHandler(_bFormVisible);
            }
            else
            {
                notifyIcon1.Visible = false;
                stop_Hotspot();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            autoconfig_change(check_autoconfig.Checked);
        }

        //-------------------NOFIFICATION METHODS----------------------------

        public void TrayHandler(bool _bShow)
        {
            if (_bShow)
                this.Show();
            else this.Hide();
        }

        private void showHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_bFormVisible)
            {
                _bFormVisible = false;
                TrayHandler(_bFormVisible);
                showHideToolStripMenuItem.Text = "Show Hotspot";
            }
            else
            {
                _bFormVisible = true;
                TrayHandler(_bFormVisible);
                showHideToolStripMenuItem.Text = "Hide Hotspot";
            }
        }

        private void startStopHotspotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startStopHotspotToolStripMenuItem.Text = (_bHotspot_Running) ? "Stop Hotspot" : "Start Hotspot";
            action_start_stop.PerformClick();
        }

        private void _contextmenu_main_Opening(object sender, CancelEventArgs e)
        {
            showHideToolStripMenuItem.Text = (_bFormVisible) ? "Hide Hotspot" : "Show Hotspot";
            startStopHotspotToolStripMenuItem.Text = (_bHotspot_Running) ? "Stop Hotspot" : "Start Hotspot";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bExit = true;
            Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Give the double-click a chance to cancel this
                singleClickTimer.Interval = (int)(SystemInformation.DoubleClickTime * 1.1);
                singleClickTimer.Start();
            }
        }

        private void singleClickTimer_Tick(object sender, EventArgs e)
        {
            showHotspotClientDetails();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Cancel the single click
                singleClickTimer.Stop();

                // do double click here
                _bFormVisible = true;
                TrayHandler(_bFormVisible);
            }
        }

        private void showHotspotClientDetails()
        {
            if (_bHotspot_Running)
                notifyicon_hotspot_on();
            else
                notifyicon_hotspot_off();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form x = new window_options(_bOptions_Check_Notify);
            x.ShowDialog();
            x.Dispose();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _bExit = true;
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form x = new window_about();
            x.ShowDialog();
            x.Dispose();
        }

        private void notifyicon_device_dc(device _device)
        {
            if (_bOptions_Check_Notify == 2)
            {
                notifyIcon1.BalloonTipTitle = "Hotspot Message";
                notifyIcon1.BalloonTipText = "Device has been disconnected\nDevice IP: " + _device._deviceIP;
                notifyIcon1.ShowBalloonTip(5000);
            }
        }

        private void notifyicon_device_new(device _device)
        {
            if (_bOptions_Check_Notify == 2)
            {
                notifyIcon1.BalloonTipTitle = "Hotspot Message";
                notifyIcon1.BalloonTipText = "New Device Connected\nDevice IP: " + _device._deviceIP;
                notifyIcon1.ShowBalloonTip(5000);
            }
        }

        private void notifyicon_hotspot_off()
        {
            if (_bOptions_Check_Notify < 3)
            {
                notifyIcon1.BalloonTipTitle = "Hotspot Message";
                notifyIcon1.BalloonTipText = "Hotspot is not running";
                notifyIcon1.ShowBalloonTip(int.MaxValue);
            }
        }

        private void notifyicon_hotspot_on()
        {
            if (_bOptions_Check_Notify < 3)
            {
                notifyIcon1.BalloonTipTitle = "Hotspot Message";
                notifyIcon1.BalloonTipText = "Hotspot is running";
                notifyIcon1.ShowBalloonTip(int.MaxValue);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nic_adapt.man_adap.InvokeMethod("Disable", null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nic_adapt.man_adap.InvokeMethod("Enable", null);
        }

        //-------------------LOG METHODS----------------------------

        private void initialize_Log()
        {
            string _temp = DateTime.Now.ToString();
            Regex rep = new Regex(@"[/\ :]");
            _temp = rep.Replace(_temp, "-");
            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");
            _log_filename = _temp + ".log";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void write_to_log(string _log_line)
        {
            write_to_log(_log_line, false);
        }

        private void write_to_log(string _log_line, bool _add_Time_Stamp)
        {
            //NetworkManagement x = new NetworkManagement()
            _logOut = new StreamWriter("logs\\" + _log_filename, true);
            if (_add_Time_Stamp)
                _logOut.WriteLine("{0} {1}\t{2}\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), _log_line);
            else
                _logOut.WriteLine(_log_line);
            _logOut.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------
        //---------------------------------------------TESTING METHODS-----------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        static NetworkManagement testnetman = new NetworkManagement();
        static List<object> onlinelist = new List<object>();
        private void button1_Click(object sender, EventArgs e)
        {
            listAdapters();
            //foreach (ManagementObject man_obj in testnetman.getAdapterList())
            //{

            //    foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            //    {  
            //        if (item.OperationalStatus == OperationalStatus.Up)
            //        {
            //            if (item.Id.ToUpper().Equals((man_obj["SettingID"].ToString()).ToUpper()))
            //            {
            //                onlinelist.Add(new nic_adap(item, man_obj));
            //                break; ;
            //            }
            //        }
            //    }
            //}
            
            //int c = 0;
            //foreach (nic_adap item in onlinelist)
            //{
            //    NetworkInterface nic = item.nic_iface;
            //    ManagementObject adap = item.man_adap;
            //    string temp1 = nic.Name;
            //    string temp2 = adap["Caption"].ToString();
            //    string temp3 = adap["Description"].ToString();
            //    string temp4 = adap["ServiceName"].ToString();
            //    string temp5 = adap["SettingID"].ToString();
            //    string temp6 = (adap["MACAddress"] == null) ? "No assigned MAC" : adap["MACAddress"].ToString();
            //    string temp7 = nic.OperationalStatus.ToString();
            //    string temp8 = (adap["DNSHostName"] == null) ? "No assigned DNSName" : adap["DNSHostName"].ToString();

            //    c++;
            //    richTextBox1.AppendText("==============================\n\nAdapter #" + c + "\n\n");
            //    richTextBox1.AppendText("Name: " + temp1 + "\nCaption: " + temp2 + "\nDescription: " + temp3 + "\nServiceName: " + temp4 + "\nSettingID: " + temp5 + "\nMACAddress: " + temp6 + "\nOperational Status = " + temp7 + "\nDNS Name = " + temp8);
            //    richTextBox1.AppendText("\n\n==============================\n\n");
            //}
            //foreach (nic_adap item in onlinelist)
            //{
            //    ManagementObject x = item.man_adap;
            //    string name = x["Caption"].ToString();
            //    if (name.Contains("Hosted"))
            //    {
            //        nic_adapt = item;
            //        testnetman.setIP(_hotspot_ip, _hotspot_subnet, "1.1.1.1");
            //        testnetman.SetNameservers(x, "8.8.8.8");
            //    }
            //}
            //foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            //{
            //    richTextBox1.AppendText("=====================\n\nAdapter - " + item.Name);
            //    foreach (UnicastIPAddressInformation ad in item.GetIPProperties().UnicastAddresses)
            //    {
            //        richTextBox1.AppendText("\n\t - " + ad.Address.ToString());
            //    }
            //    richTextBox1.AppendText("\n======================");
            //}
        }

        public void listAdapters()
        {
            string name = Dns.GetHostName();
            try
            {
                IPAddress[] addrs = Dns.Resolve(name).AddressList;
                foreach (IPAddress addr in addrs)
                    richTextBox1.AppendText(name + "/" + addr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            System.Configuration.
            //SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            //ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            //using ()
            //{

            //}
            //foreach (ManagementObject item in searchProcedure.Get())
            //{
            //    NetworkAdapter temp = item["Caption"].ToString();
            //    if (((string)item["Caption"]).Contains("Hosted"))
            //    {
            //        item.InvokeMethod("Disable", null);
            //    }
            //}
            //using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"select * from Win32_NetworkAdapter"))
            //{
            //    ManagementObjectCollection results = searcher.Get();
            //    foreach (ManagementObject obj in results)
            //    {
            //        richTextBox1.AppendText("Found adapter {0} : " + obj["Caption"]);
            //        richTextBox1.AppendText("Disabling adapter ...");
            //        object[] param = new object[0];
            //        obj.InvokeMethod("Disable", param);
            //        richTextBox1.AppendText("Done.");
            //    }
            //    Console.ReadLine();
            //}
        }
    }
}
