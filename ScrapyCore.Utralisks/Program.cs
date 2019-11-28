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
            Bootstrap bootstrap = Bootstrap.DefaultInstance;
            UtralisksSystemController utralisksSystemController = new UtralisksSystemController(bootstrap, bootstrap.HostedMachine);
            utralisksSystemController.Start();
        }
    }
}
