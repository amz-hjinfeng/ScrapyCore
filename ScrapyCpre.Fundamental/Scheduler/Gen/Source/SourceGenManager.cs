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

        private SourceGenManager()
        {
            SourceGenerators = new Dictionary<string, ISourceGen>();
        }

        public static Dictionary<string, SourceGenAttribute> SourceGenMeta { get; set; }
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
                .ToDictionary(x => x.SourceGenAttr.Name, x =>
                {
                    x.SourceGenAttr.SourceGanType = x.Type;
                    return x.SourceGenAttr;
                });

        }


        private Dictionary<string, ISourceGen> SourceGenerators { get; set; }


        private ISourceGen GetSourceGen(string genType)
        {
            if (!SourceGenerators.ContainsKey(genType))
            {
                if (SourceGenMeta.ContainsKey(genType))
                {
                    SourceGenerators[genType] =
                        Activator.CreateInstance(SourceGenMeta[genType].SourceGanType) as ISourceGen;
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
                var param = sourceGen.GetParameter(item.Parameters, Guid.NewGuid().ToString());
                SourceObject sourceObject = new SourceObject()
                {
                    Parameters = param.Parameter,
                    Type = param.SourceType
                };
                ScrapySource scrapySource = new ScrapySource()
                {
                    GenType = sourceGen.GenType,
                    Name = item.Name,
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
