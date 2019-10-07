using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Text.RegularExpressions;

namespace ScrapyCore.Core.HostMachine
{
    public abstract class HostedMachine : IHostedMachine
    {
        public abstract string Id { get; }
        public abstract string PrivateIpAddress { get; }
        public abstract string PublicIpAddress { get; }
        public string OperatingSystem => RuntimeInformation.OSDescription;
        public abstract string HostName { get; }


        protected virtual string GetPublicIpAddressViaSohu()
        {
            string targetUrl = "http://txt.go.sohu.com/ip/soip";
            using (WebClient webClient = new WebClient())
            {
                Regex regex = new Regex(@"\d+.\d+.\d+.\d+", RegexOptions.Compiled);
                string result = webClient.DownloadString(targetUrl);
                string ip = regex.Match(result).Value;
                return ip;
            }

        }
    }
}
