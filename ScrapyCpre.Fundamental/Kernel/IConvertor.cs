using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel
{
    public interface IConvertor
    {
        ContextData Convert(ContextData contentData);
    }
}
