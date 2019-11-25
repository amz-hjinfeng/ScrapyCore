using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Scheduler
{
    public interface IScheduler
    {
        Task ScheduleNew(ScheduleMessage scheduleMessage);


        // TODO: Temp only
        Task ScheduleBack(ScrapySource source, PlatformModel platformModel, List<string> urls, ScheduleMessage scheduleMessage);

        Task ScheduleNew(ScheduleMessage scheduleMessage, PlatformModel platformModel);
    }
}
