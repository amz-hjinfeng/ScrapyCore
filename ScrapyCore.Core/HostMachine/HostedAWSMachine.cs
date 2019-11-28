using Amazon.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ScrapyCore.Core.HostMachine
{
    public class HostedAWSMachine : HostedMachine
    {
        public override string Id => EC2InstanceMetadata.InstanceId;

        public override string PrivateIpAddress => EC2InstanceMetadata.PrivateIpAddress;

        public override string PublicIpAddress => GetPublicAddress();

        public override string HostName => EC2InstanceMetadata.Hostname;

        public string GetPublicAddress()
        {
            try
            {
                return GetContect("http://169.254.169.254/latest/meta-data/public-ipv4");
            }
            catch (Exception)
            {
                return "-";
            }
        }

        private static string GetContect(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(url);
            }
        }
    }
}
