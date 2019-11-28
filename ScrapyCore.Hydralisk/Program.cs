using ScrapyCore.Core;

namespace ScrapyCore.Hydralisk
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap bootstrap = Bootstrap.DefaultInstance;
            HydraliskSystemController hydraliskSystemController = new HydraliskSystemController(bootstrap, bootstrap.HostedMachine);
            hydraliskSystemController.Start();
        }
    }
}
