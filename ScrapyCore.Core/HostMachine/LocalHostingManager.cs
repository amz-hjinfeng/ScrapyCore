using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.HostMachine
{
    public class LocalHostingManager : IHostingManager
    {
        public Task Terminate(IHostedMachine hostedMachine)
        {
            Process.GetCurrentProcess().Kill();
            return Task.CompletedTask;
        }
    }
}
