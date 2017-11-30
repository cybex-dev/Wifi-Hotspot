using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace _hotspot_core
{
    public class hotspot
    {
        public static bool _bHotspot_Running = false;                         //flag for start/stop hotspt
        public static Thread _loop_main;                                       //thread executed after hotspot is started, running :  void loop()
        public static List<device> _ipcol_all_devices = new List<device>();
        public static List<device> _ipcol_all_devices_old = new List<device>();
        public static List<device> _devices_new = new List<device>();
        public static List<device> _devices_dc = new List<device>();

        public string _ssid = "hotspot", _passphrase = "12345678", _mode = "allow", _hotspot_ip = "192.168.0.1", _hotspot_subnet = "255.255.255.0", // user input options for hotspot
            _ip_local_vwifi_network = "192.168.0";                                    //virtual wifi network i.e. first 3 combinations in _hotspot_ip
        public const string _autoconfig_ip_hotspot = "192.168.0.1",
            _autoconfig_subnet_hotspot = "255.255.255.0";               //fallback autoconfig for hotspot
        public NetworkInterface _nic_vwifi;                                    //interface of virtual interface
                                                                        //TImer for single click double click issue - notification icon

        public hotspot()
        {
        }

        public hotspot(string _ip, string _subnet)
        {
            Hotspot_ip = _ip;
            Hotspot_subnet = _subnet;
        }

        public bool start_Hotspot()
        {
            string _netsh_arg = "netsh wlan set hostednetwork ssid=" + Ssid + " key=" + Passphrase + " mode=" + Mode + " & netsh wlan start hostednetwork",

             _assignip = "netsh interface ip set address \"Local Area Connection * 12\" static " + Hotspot_ip + " " + Hotspot_subnet + " " + Hotspot_ip;

            Process _netsh_hotspot_start = create_Process(_netsh_arg);
            launch_process(_netsh_hotspot_start);


            string _hotspot_error = _netsh_hotspot_start.StandardError.ReadToEnd(),
            _hotspot_output = _netsh_hotspot_start.StandardOutput.ReadToEnd();

            if (_hotspot_error == "" && new Regex("The hosted network started").IsMatch(_hotspot_output))
            {
                preloop();
                return true;
            }
            else return false;
        }

        public bool stop_Hotspot()
        {
            string _netsh_arg_stop = "netsh wlan stop hostednetwork";
            Process _netsh_hotspot_stop = create_Process(_netsh_arg_stop);
            launch_process(_netsh_hotspot_stop);

            string _hotspot_error = _netsh_hotspot_stop.StandardError.ReadToEnd();
            string _hotspot_output = _netsh_hotspot_stop.StandardOutput.ReadToEnd();

            return (_hotspot_error == "" || new Regex("The hosted network stopped").IsMatch(_hotspot_output)) ? true : false;
        }

        public void preloop()
        {
            //one time run in loop
            //should check for new users connected

            //update_Connected_Devices();

            adapterIPandProp();

            _loop_main = new Thread(new ThreadStart(loop));
            _loop_main.Start();

            //display user info
        }

        public void loop()
        {
            while (_bHotspot_Running)
            {
                update_Connected_Devices();
                Thread.Sleep(1000);
            }
        }

        //-----------------SECONDARY METHODS-----------------------

        public bool compat_check()
        {
            Regex _hostednetworkcheck = new Regex("Hosted network supported  : Yes");
            string _netsh_show_drivers = "netsh wlan show drivers";
            string _drivers_output, _drivers_error;

            Process _netsh_compat = create_Process(_netsh_show_drivers);
            launch_process(_netsh_compat);

            _drivers_error = _netsh_compat.StandardError.ReadToEnd();
            _drivers_output = _netsh_compat.StandardOutput.ReadToEnd();
            return (_hostednetworkcheck.IsMatch(_drivers_output)) ? true : false;

        }

        public void update_Connected_Devices()
        {
            _ipcol_all_devices = get_All_Connected_Devices(_nic_vwifi);
            check_new_devices(_ipcol_all_devices);
            check_dc_devices(_ipcol_all_devices);
            _ipcol_all_devices_old = _ipcol_all_devices;
        }

        public void get_IP_Host_Info(IPAddress IP)
        {
            IPHostEntry _hostname = Dns.GetHostEntry(IP);
        }

        public List<device> get_All_Connected_Devices(NetworkInterface _nic_vwifi)
        {
            string _arp_string = "arp -a | findstr -i " + Ip_local_vwifi_network + " | findstr /V 255 | findstr /V " + Hotspot_ip;
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

        public void check_dc_devices(List<device> _ipcol_all_devices)
        {
            _devices_dc.Clear();
            bool bCheckDC = false;
            foreach (device _current_dc in _ipcol_all_devices_old)
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
                if (!bCheckDC) _devices_dc.Add(_current_dc);
            }
        }

        public void check_new_devices(List<device> _devices_alive)
        {
            _devices_new.Clear();
            bool _bCheckNew = false;
            foreach (device _current_new in _devices_alive)
            {
                _bCheckNew = false;
                foreach (device _current_alive in _ipcol_all_devices_old)
                {
                    if (_current_new.Equals(_current_alive))
                    {
                        _bCheckNew = true;
                        break;
                    }
                }
                if (!_bCheckNew) _devices_new.Add(_current_new);
            }
        }

        //-----------------UTILITY METHODS--------------------------
        
        public string Ssid
        {
            get
            {
                return _ssid;
            }

            set
            {
                _ssid = value;
            }
        }

        public string Passphrase
        {
            get
            {
                return _passphrase;
            }

            set
            {
                _passphrase = value;
            }
        }

        public string Mode
        {
            get
            {
                return _mode;
            }

            set
            {
                _mode = value;
            }
        }

        public string Hotspot_ip
        {
            get
            {
                return _hotspot_ip;
            }

            set
            {
                _hotspot_ip = value;
            }
        }

        public string Hotspot_subnet
        {
            get
            {
                return _hotspot_subnet;
            }

            set
            {
                _hotspot_subnet = value;
            }
        }

        public string Ip_local_vwifi_network
        {
            get
            {
                return _ip_local_vwifi_network;
            }

            set
            {
                _ip_local_vwifi_network = value;
            }
        }

        public Process create_Process(string _arg)
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

        public void launch_process(Process _proc_Start)
        {
            _proc_Start.Start();
        }

        public List<device> process_arp_output(List<string> _arp_raw_data)
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

        public List<device> check_Alive_devices(List<device> _clients)
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
                            item._count_ping_timeout++;
                        }
                        break;

                    case IPStatus.Success:
                        {
                            item._sum_ping_time = item._sum_ping_time + (int)_client_reply.RoundtripTime;
                            _alive.Add(item);
                            break;
                        }
                }
                item._count_ping++;
            }
            return _alive;
        }

        public List<device> compareClientList(List<device> _clients)
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

        public void adapterIPandProp()
        {
            Ip_local_vwifi_network = _hotspot_ip.Substring(0, _hotspot_ip.LastIndexOf('.'));

            NetworkInterface[] t = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && _hotspot_ip == ip.Address.ToString())
                    {
                        _nic_vwifi = item;
                    }
                }
            }
        }

        public void autoconfig_change(bool v)
        {
            if (v)
            {
                Hotspot_ip = _autoconfig_ip_hotspot;
                Hotspot_subnet = _autoconfig_subnet_hotspot;
            }
        }
    }

    public class device
    {
        public IPAddress _deviceIP;
        public string _deviceMAC;
        public PingReply _devicePing;
        public int _count_ping_timeout = 0;
        public int _count_ping = 0;
        public int _sum_ping_time = 0;

        public device(IPAddress ip, string mac)
        {
            _deviceIP = ip;
            _deviceMAC = mac;
            _devicePing = null;
        }

        public int get_Device_Ping()
        {
            return (_devicePing == null) ? -1 : (int)_devicePing.RoundtripTime;
        }

        public override string ToString()
        {
            return "DEVICE DETAILS:\n" + _devicePing.RoundtripTime.ToString() + "\n" + _deviceIP.ToString() + "\n " + _deviceMAC;
        }

        public bool Equals(device obj)
        {
            return this._deviceMAC.Equals(obj._deviceMAC);
        }

        public double get_average_ping()
        {
            return Math.Round(_sum_ping_time / _count_ping * 1.00, 2);
        }

        public double get_loss()
        {
            return Math.Round(_count_ping_timeout / _count_ping * 1.00, 2);
        }

    }
}
