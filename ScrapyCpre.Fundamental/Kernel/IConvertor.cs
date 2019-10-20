using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel
{
    public interface IConvertor
    {
        ContentData Convert(ContentData contentData);
    }
}
