using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using System;

namespace ScrapyCore.Kerrigan
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap bootstrap = new Bootstrap();
            IHostedMachine hostedMachine = new HostedLocalMachine();
            KerriganSystemController kerriganController = new KerriganSystemController(bootstrap, hostedMachine);
            kerriganController.Start();
        }
    }
}
