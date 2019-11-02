using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExtractorAttribute : Attribute
    {
        public ExtractorAttribute(string extractorType)
        {
            ExtractorType = extractorType;
        }
        public string ExtractorType { get; }
    }
}
