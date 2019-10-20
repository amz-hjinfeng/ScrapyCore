using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel
{
    public abstract class Convertor : IConvertor
    {
        public abstract ContextData Convert(ContextData contentData);
    }
}
