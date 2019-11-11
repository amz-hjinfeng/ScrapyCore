using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldAttribute : Attribute
    {
        public string DefaultValue { get; set; }

        public string AcceptTypeName { get; set; }

        public string FieldName { get; set; }
    }
}
