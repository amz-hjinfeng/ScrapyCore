using ScrapyCore.Core;
using ScrapyCore.Fundamental.Scheduler.impls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class ScheduleServiceFactory : IServiceFactory<IScheduler, ISchedulerConfigure>
    {
        private static ScheduleServiceFactory _factory;
        public static ScheduleServiceFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new ScheduleServiceFactory();
                return _factory;
            }
        }

        private Dictionary<string, Type> SchedulerTypes;
        private ScheduleServiceFactory()
        {
            SchedulerTypes = new Dictionary<string, Type>();
            SchedulerTypes["DelayScheduler"] = typeof(DelayScheduler);
        }

        public IScheduler GetService(ISchedulerConfigure configure)
        {
            if (SchedulerTypes.ContainsKey(configure.Type))
            {
                var schedulerType = SchedulerTypes[configure.Type];
                return Activator.CreateInstance(schedulerType, new object[] { configure }) as IScheduler;
            }
            return null;
        }

        public IList<string> GetServiceKeys()
        {
            return SchedulerTypes.Keys.ToList();
        }
    }
}
