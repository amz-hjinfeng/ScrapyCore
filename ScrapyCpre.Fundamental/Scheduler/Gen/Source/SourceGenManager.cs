using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ScrapyCore.Fundamental.Kernel.Extract;
using ScrapyCore.Fundamental.Scheduler.Attributes;
using ScrapyCore.Fundamental.Scheduler.Models;

namespace ScrapyCore.Fundamental.Scheduler.Gen
{
    public class SourceGenManager
    {

        private static SourceGenManager instance;

        public static SourceGenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SourceGenManager();
                return instance;
            }

        }

        private static Dictionary<string, Type> SourceGenMeta { get; set; }
        static SourceGenManager()
        {
            SourceGenMeta = Assembly.GetAssembly(typeof(SourceGenManager))
                .GetTypes()
                .Where(x => !x.IsInterface)
                .Where(x => !x.IsAbstract)
                .Select(x => new
                {
                    SourceGenAttr = x.GetCustomAttribute<SourceGenAttribute>(),
                    Type = x
                }).Where(x => x.SourceGenAttr != null)
                .ToDictionary(x => x.SourceGenAttr.Name, x => x.Type);

        }


        private Dictionary<string, ISourceGen> SourceGenerators { get; set; }


        private ISourceGen GetSourceGen(string genType)
        {
            if (!SourceGenerators.ContainsKey(genType))
            {
                if (SourceGenMeta.ContainsKey(genType))
                {
                    SourceGenerators[genType] =
                        Activator.CreateInstance(SourceGenMeta[genType]) as ISourceGen;
                    return SourceGenerators[genType];
                }
                return null;
            }
            else
            {
                return SourceGenerators[genType];
            }

        }


        public Dictionary<string, ScrapySource> GenerateSource(ScheduleSource[] scheduleSources, string messageId)
        {
            Dictionary<string, ScrapySource> result = new Dictionary<string, ScrapySource>();
            foreach (var item in scheduleSources)
            {

                ISourceGen sourceGen = GetSourceGen(item.Type);
                var param = sourceGen.GetParameter(item.Parameters);
                SourceObject sourceObject = new SourceObject()
                {
                    Parameters = param.Parameter,
                    Type = sourceGen.GenType
                };
                ScrapySource scrapySource = new ScrapySource()
                {
                    JobId = Guid.NewGuid().ToString(),
                    MessageId = messageId,
                    Source = sourceObject,
                    SaveTo = "/transform/" + param.RecommendLocation + ".dat"
                };
                result.Add(item.Name, scrapySource);
            }
            return result;
        }
    }
}
