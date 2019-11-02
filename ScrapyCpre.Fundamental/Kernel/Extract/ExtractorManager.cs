using ScrapyCore.Core;
using ScrapyCore.Core.Injection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public class ExtractorManager
    {
        private readonly InjectionProvider injectionProvider;
        public Dictionary<string, Type> SourceTypeMapping { get; private set; }

        public ExtractorManager(InjectionProvider injectionProvider)
        {
            this.injectionProvider = injectionProvider;
        }

        public IExtractor GetExtrator(string type)
        {
            return this.injectionProvider.CreateInstance(SourceTypeMapping[type]) as IExtractor;
        }
    }
}
