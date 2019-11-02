using ScrapyCore.Core;
using ScrapyCore.Core.Injection;
using ScrapyCore.Fundamental.Kernel.Extract.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public class ExtractorManager : IExtractorManager
    {
        private readonly IInjectionProvider injectionProvider;
        public static Dictionary<string, Type> SourceTypeMapping { get; private set; }

        public Dictionary<string, IExtractor> Extractors { get; set; }

        static ExtractorManager()
        {
            SourceTypeMapping = new Dictionary<string, Type>();
            SourceTypeMapping = Assembly.GetAssembly(typeof(ExtractorManager))
                  .GetTypes()
                  .Where(t => !t.IsAbstract)
                  .Where(t => !t.IsInterface)
                  .Select(t => new
                  {
                      Type = t,
                      ExtractorAttribute = t.GetCustomAttribute<ExtractorAttribute>()
                  })
                  .Where(t => t.ExtractorAttribute != null)
                  .ToDictionary(t => t.ExtractorAttribute.ExtractorType, t => t.Type);
        }


        public ExtractorManager(IInjectionProvider injectionProvider)
        {
            this.injectionProvider = injectionProvider;
            Extractors = new Dictionary<string, IExtractor>();
        }

        public IExtractor GetExtrator(string type)
        {
            if (!Extractors.ContainsKey(type))
            {
                var instance = this.injectionProvider.CreateInstance(SourceTypeMapping[type]) as IExtractor;
                Extractors[type] = instance;
            }
            return Extractors[type];
        }
    }
}
