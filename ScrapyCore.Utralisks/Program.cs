using ScrapyCore.Core;
using log4net;
using System;
using ScrapyCore.Core.HostMachine;

namespace ScrapyCore.Utralisks
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap bootstrap = new Bootstrap();
            IHostedMachine hostedMachine = new HostedLocalMachine();
            UtralisksSystemController utralisksSystemController = new UtralisksSystemController(bootstrap, hostedMachine);
            utralisksSystemController.Start();
        }
    }
}
