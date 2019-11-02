using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Injection
{
    public class InjectAttribute : Attribute
    {
        public string Name { get; }

        public InjectAttribute(string name)
        {
            this.Name = name;
        }
    }
}
