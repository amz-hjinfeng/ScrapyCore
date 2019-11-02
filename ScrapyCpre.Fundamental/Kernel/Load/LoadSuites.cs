using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadSuites
    {
        public object ConfigureFactory { get; set; }
        public object ServiceFactory { get; set; }
        public Type ProviderType { get; set; }
    }
}
