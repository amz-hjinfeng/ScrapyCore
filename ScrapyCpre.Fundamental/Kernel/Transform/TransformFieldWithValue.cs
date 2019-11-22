using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Transform
{
    public class TransformFieldWithValue
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public List<string> Value { get; set; }

        public TransformFieldWithValue()
        {
            Value = new List<string>();
        }
    }
}
