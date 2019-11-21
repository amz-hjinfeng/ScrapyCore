using ScrapyCore.Core;
using ScrapyCore.Core.Injection;
using ScrapyCore.Fundamental.Kernel.Extract.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public class ExtractorManager : IExtractorManager
    {
        private readonly IInjectionProvider injectionProvider;
        public static Dictionary<string, ExtractorAttribute> SourceTypeMapping { get; private set; }

        public ConcurrentDictionary<string, IExtractor> Extractors { get; set; }

        static ExtractorManager()
        {
            SourceTypeMapping = new Dictionary<string, ExtractorAttribute>();
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
                  .ToDictionary(t => t.ExtractorAttribute.Name, t =>
                  {
                      t.ExtractorAttribute.ExtractorType = t.Type;
                      return t.ExtractorAttribute;
                  });
        }


        public ExtractorManager(IInjectionProvider injectionProvider)
        {
            this.injectionProvider = injectionProvider;
            Extractors = new ConcurrentDictionary<string, IExtractor>();
        }

        public IExtractor GetExtrator(string type)
        {
            if (!Extractors.ContainsKey(type))
            {
                var instance = this.injectionProvider.CreateInstance(SourceTypeMapping[type].ExtractorType) as IExtractor;
                Extractors[type] = instance;
            }
            return Extractors[type];
        }
    }
}
