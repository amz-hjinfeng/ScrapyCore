using log4net;
using ScrapyCore.Core.Metric;
using ScrapyCore.Core.Metric.Reporter;
using System;
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
        public abstract IMessagePipline MessagePipline { get; }

        public virtual IWorkingMessageProcessor WorkingProcessor { get; protected set; }

        private IMetricReporter metricReporter;

        public SystemController(Bootstrap bootstrap)
        {
            this.bootstrap = bootstrap;
            switch (bootstrap.GetVariableSet("environment"))
            {
                case "aws": metricReporter = new AWSCloudWatchReporter(); break;
                default: metricReporter = new DefaultReporter(); break;
            }

        }

        /// <summary>
        /// Processor that Trigger the
        /// </summary>
        protected abstract void Processor();

        protected abstract void ProvisionWebHost();

        /// <summary>
        /// Called by the Main Thread
        /// </summary>
        protected abstract void HeartBeatProcessor();

        private void RollingHeartBeat()
        {
            while (SYSTEM_STOP != SystemStatus)
            {
                HeartBeatProcessor();
                metricReporter.Report(MetricCollections.Default).Wait();
                Random random = new Random(DateTime.Now.Millisecond);
                Thread.Sleep(random.Next(1000, 1500));
            }
        }


        public virtual void Start()
        {
            SystemStatus = SYSTEM_ON;
            bootstrap.Provisioning.ThreadManager.DoWork(ProvisionWebHost);
            bootstrap.Provisioning.ThreadManager.DoWork(RollingHeartBeat);
            while (SystemStatus != SYSTEM_STOP)
            {
                if (SystemStatus == SYSTEM_PAUSE) continue;
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
