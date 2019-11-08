using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Attributes
{
    public class SchedulerAttribute : Attribute
    {
        public string Name { get; private set; }
        public Type ParameterType { get; private set; }

        public SchedulerAttribute(string name, Type parameterType)
        {
            Name = name;
            ParameterType = parameterType;
        }


    }
}
