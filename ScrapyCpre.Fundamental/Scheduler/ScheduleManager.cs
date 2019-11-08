using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class ScheduleManager
    {
        public IScheduler GetScheduler(string schedulerName)
        {
            return null;
        }

        public IScheduler GetDefaultScheduler(IPlatformExit platformExit, ICache coreCache)
        {
            return new DefaultSchueduler(platformExit, coreCache);
        }


        private static ScheduleManager manager;
        public static ScheduleManager Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = new ScheduleManager();
                }
                return manager;
            }
        }
        private ScheduleManager()
        {

        }
    }
}
