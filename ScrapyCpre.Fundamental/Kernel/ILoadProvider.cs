using ScrapyCore.Fundamental.Kernel.Load;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel
{
    public interface ILoadProvider
    {
        Task Load(Stream content, LoadContext ldContext);
    }
}
