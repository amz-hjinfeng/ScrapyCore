using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.HostMachine
{
    public class HotedMachineManager
    {
        private static Dictionary<string, Lazy<IHostedMachine>> HostedMachines { get; set; }

        public static IHostedMachine GetHostedMachine(string env)
        {
            if (HostedMachines.ContainsKey(env))
            {
                return HostedMachines[env].Value;
            }
            else
            {
                return GetHostedMachine("local");
            }
        }

        static HotedMachineManager()
        {
            HostedMachines.Add("aws", new Lazy<IHostedMachine>(() => new HostedAWSMachine()));
            HostedMachines.Add("local", new Lazy<IHostedMachine>(() => new HostedLocalMachine()));
        }
    }
}
