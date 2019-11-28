using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.HostMachine
{
    public interface IHostingManager
    {
        Task Terminate(IHostedMachine hostedMachine);
    }
}
