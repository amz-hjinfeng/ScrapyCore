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

        Task ScheduleBack(string messageId, string jobId);
    }
}
