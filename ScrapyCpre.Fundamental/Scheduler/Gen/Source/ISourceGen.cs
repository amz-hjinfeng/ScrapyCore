using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Gen
{
    public interface ISourceGen
    {
        ParamWithId GetParameter(object templateParameter);
        string GenType { get; }
    }
}
