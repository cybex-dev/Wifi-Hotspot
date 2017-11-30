using System;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;

namespace hotspot
{
    class device
    {
        public IPAddress _deviceIP;
        public string _deviceMAC;
        public PingReply _devicePing;
        public Color _deviceStatus = Color.White;
        public int _count_ping_timeout = 0;
        public int _count_ping = 0;
        public int _sum_ping_time = 0;

        public device(IPAddress ip, string mac)
        {
            _deviceIP = ip;
            _deviceMAC = mac;
            _devicePing = null;
        }
         
        public Color deviceStatus
        {
            get { return _deviceStatus; }
            set { _deviceStatus = value; }
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
