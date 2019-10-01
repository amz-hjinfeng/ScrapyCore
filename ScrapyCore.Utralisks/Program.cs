using ScrapyCore.Core;
using log4net;
using System;

namespace ScrapyCore.Utralisks
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap bootstrap = new Bootstrap();
            UtralisksSystemController utralisksSystemController = new UtralisksSystemController(bootstrap);
            utralisksSystemController.Start();
        }
    }
}
