using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using System;

namespace ScrapyCore.Kerrigan
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap bootstrap = Bootstrap.DefaultInstance;
            KerriganSystemController kerriganController = new KerriganSystemController(bootstrap, bootstrap.HostedMachine);
            kerriganController.Start();
        }
    }
}
