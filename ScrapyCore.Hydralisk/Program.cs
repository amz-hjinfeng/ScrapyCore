using ScrapyCore.Core;
using ScrapyCore.Core.HostMachine;
using System;

namespace ScrapyCore.Hydralisk
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap bootstrap = new Bootstrap();
            IHostedMachine hostedMachine = new HostedLocalMachine();
            HydraliskSystemController hydraliskSystemController = new HydraliskSystemController(bootstrap, hostedMachine);
            hydraliskSystemController.Start();
        }
    }
}
