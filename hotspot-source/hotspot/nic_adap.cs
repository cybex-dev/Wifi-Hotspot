using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.NetworkInformation;

namespace hotspot
{
    class nic_adap
    {
        NetworkInterface _nic_iface = null;
        ManagementObject _man_adap = null;

        public nic_adap(NetworkInterface nic, ManagementObject man_obj)
        {
            _nic_iface = nic;
            _man_adap = man_obj;
        }

        public NetworkInterface nic_iface
        {
            get { return _nic_iface; }
            set { _nic_iface = value; }
        }

        public ManagementObject man_adap
        {
            get { return _man_adap; }
            set { _man_adap = value; }
        }
    }
}
