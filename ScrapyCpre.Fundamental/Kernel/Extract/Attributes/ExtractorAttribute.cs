using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExtractorAttribute : Attribute
    {
        public Type ParameterType { get; set; }

        public ExtractorAttribute(string extractorName)
        {
            Name = extractorName;
        }
        public string Name { get; }

        public Type ExtractorType { get; set; }
    }
}
