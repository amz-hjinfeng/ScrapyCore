using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class ScheduleConfigureFactory : IConfigurationFactory<ISchedulerConfigure>
    {
        private static ScheduleConfigureFactory _factory;
        public static ScheduleConfigureFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new ScheduleConfigureFactory();
                return _factory;
            }
        }

        public ISchedulerConfigure CreateConfigure(IStorage storage, string path)
        {
            var configureModel = JsonConvert.DeserializeObject<SchedulerConfigureModel>(storage.GetString(path));
            return configureModel;
        }
    }
}
