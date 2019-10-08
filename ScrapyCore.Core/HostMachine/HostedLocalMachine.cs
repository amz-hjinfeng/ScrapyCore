using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace ScrapyCore.Core.HostMachine
{
    public class HostedLocalMachine : HostedMachine
    {
        private readonly string privateIpAddress;
        private readonly string id;
        public override string Id => id;
        public override string PrivateIpAddress => privateIpAddress;
        public override string PublicIpAddress => GetPublicIpAddressViaSohu();
        public override string HostName => Dns.GetHostName();
        public HostedLocalMachine()
        {
            id = Guid.NewGuid().ToString();
            privateIpAddress = NetworkInterface.GetAllNetworkInterfaces()
                                                               .Select(p => p.GetIPProperties())
                                                               .SelectMany(p => p.UnicastAddresses)
                                                               .Where(p => p.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(p.Address))
                                                               .FirstOrDefault()?.Address.ToString();
        }

    }
}
