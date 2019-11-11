using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Attributes
{
    public class SourceGenAttribute : Attribute
    {
        public SourceGenAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public Type ParameterType { get; set; }

        public Type SourceGanType { get; set; }

    }
}
