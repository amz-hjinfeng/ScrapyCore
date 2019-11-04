using ScrapyCore.Fundamental.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Transform
{
    public class FieldDefinition
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public bool CanNullable { get; set; }

        public ConvertorNavigator[] ConvertorNavigators { get; set; }
    }
}
