using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Configure.Storage;
using ScrapyCore.Core.Storages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public abstract class LoadProvider : ILoadProvider
    {
        public abstract Task Load(Stream content, object paramter);
    }
}
