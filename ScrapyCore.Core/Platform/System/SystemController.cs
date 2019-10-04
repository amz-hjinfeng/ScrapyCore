using log4net;
using System.Threading;

namespace ScrapyCore.Core.Platform.System
{
    public abstract class SystemController : ISystemController
    {
        protected static ILog logger = LogManager.GetLogger("Scrapy-Repo", typeof(SystemController));
        const int SYSTEM_ON = 1;
        const int SYSTEM_PAUSE = 2;
        const int SYSTEM_STOP = 0;
        protected readonly Bootstrap bootstrap;

        protected int SystemStatus { get; set; }

        public SystemController(Bootstrap bootstrap)
        {
            this.bootstrap = bootstrap;
        }

        /// <summary>
        /// Processor that Trigger the
        /// </summary>
        protected abstract void Processor();

        protected abstract void ProvisionWebHost();

        public virtual void Start()
        {
            SystemStatus = SYSTEM_ON;

            bootstrap.Provisioning.ThreadManager.DoWork(ProvisionWebHost);
            while (SystemStatus != SYSTEM_STOP)
            {
                if (SystemStatus == SYSTEM_PAUSE)
                {
                    Thread.Sleep(10);
                    continue;
                }
                bootstrap.Provisioning.ThreadManager.DoWork(Processor);
            }
        }
        public virtual void Pause()
        {
            this.SystemStatus = SYSTEM_PAUSE;
        }
        public virtual void Stop()
        {
            this.SystemStatus = SYSTEM_STOP;
        }
    }
}
