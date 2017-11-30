using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace hotspot
{
    class NetworkManagement
    {
        /// <summary>
        /// Set's a new IP Address and it's Submask of the local machine
        /// </summary>
        /// <param name="ip_address">The IP Address</param>
        /// <param name="subnet_mask">The Submask IP Address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        /// 


        public List<ManagementObject> getAdapterList()
        {
            List<ManagementObject> adapterlist = new List<ManagementObject>();
            foreach (ManagementObject item in new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances())
            {
                adapterlist.Add(item);
            }
            return adapterlist;
        }

        /// <summary>
        /// Set's a new IP Address and it's Submask of the local machine
        /// </summary>
        /// <param name="ipAddress">The IP Address</param>
        /// <param name="subnetMask">The Submask IP Address</param>
        /// <param name="gateway">The gateway.</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        /// 

        public void setIP(string IPAddress, string SubnetMask, string Gateway)
        {

            ManagementClass objMC = new ManagementClass(
                "Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();


            foreach (ManagementObject objMO in objMOC)
            {

                if (!(bool)objMO["IPEnabled"])
                    continue;



                try
                {
                    ManagementBaseObject objNewIP = null;
                    ManagementBaseObject objSetIP = null;
                    ManagementBaseObject objNewGate = null;


                    objNewIP = objMO.GetMethodParameters("EnableStatic");
                    objNewGate = objMO.GetMethodParameters("SetGateways");



                    //Set DefaultGateway
                    objNewGate["DefaultIPGateway"] = new string[] { Gateway };
                    objNewGate["GatewayCostMetric"] = new int[] { 1 };


                    //Set IPAddress and Subnet Mask
                    objNewIP["IPAddress"] = new string[] { IPAddress };
                    objNewIP["SubnetMask"] = new string[] { SubnetMask };

                    objSetIP = objMO.InvokeMethod("EnableStatic", objNewIP, null);
                    objSetIP = objMO.InvokeMethod("SetGateways", objNewGate, null);



                    Console.WriteLine(
                       "Updated IPAddress, SubnetMask and Default Gateway!");



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Set IP : " + ex.Message);
                }
            }
        }

        //public void SetIP(ManagementObject nic, string ipAddress, string subnetMask, string gateway)
        //{
        //    //using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
        //    //{
        //    //    using (var networkConfigs = networkConfigMng.GetInstances())
        //    //    {
        //    //        foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(managementObject => (bool)managementObject["IPEnabled"]))
        //    //        {
        //    using (var newIP = nic.GetMethodParameters("EnableStatic"))
        //    {
        //        // Set new IP address and subnet if needed
        //        if ((!String.IsNullOrEmpty(ipAddress)) || (!String.IsNullOrEmpty(subnetMask)))
        //        {
        //            if (!String.IsNullOrEmpty(ipAddress))
        //            {
        //                newIP["IPAddress"] = new[] { ipAddress };
        //            }

            //            if (!String.IsNullOrEmpty(subnetMask))
            //            {
            //                newIP["SubnetMask"] = new[] { subnetMask };
            //            }

            //            nic.InvokeMethod("EnableStatic", newIP, null);
            //        }

            //        // Set mew gateway if needed
            //        if (!String.IsNullOrEmpty(gateway))
            //        {
            //            using (var newGateway = nic.GetMethodParameters("SetGateways"))
            //            {
            //                newGateway["DefaultIPGateway"] = new[] { newGateway };
            //                newGateway["GatewayCostMetric"] = new[] { 1 };
            //                nic.InvokeMethod("SetGateways", newGateway, null);
            //            }
            //        }
            //    }
            //        //        }
            //        //    }
            //        //}
            //    //}
            //}

            /// <summary>
            /// Set's the DNS Server of the local machine
            /// </summary>
            /// <param name="nic">NIC address</param>
            /// <param name="dnsServers">Comma seperated list of DNS server addresses</param>
            /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetNameservers(ManagementObject nic, string dnsServers)
        {
            //using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            //{
            //    using (var networkConfigs = networkConfigMng.GetInstances())
            //    {
            //        foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(objMO => (bool)objMO["IPEnabled"] && objMO["Caption"].Equals(nic)))
            //        {
                        using (var newDNS = nic.GetMethodParameters("SetDNSServerSearchOrder"))
                        {
                            newDNS["DNSServerSearchOrder"] = dnsServers.Split(',');
                            nic.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
            //        }
            //    }
            //}
        }   
    }
}
