using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.HostMachine
{
    public interface IHostedMachine
    {
        string Id { get; }

        string HostName { get; }

        string PrivateIpAddress { get; }

        string PublicIpAddress { get; }

        string OperatingSystem { get; }
    }
}
