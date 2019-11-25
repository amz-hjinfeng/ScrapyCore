using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScrapyCore.Core.ElasticSearch
{
    public class ElasticSearchTypeManager
    {
        public static Dictionary<string, Type> NameTypes { get; set; }

        static ElasticSearchTypeManager()
        {
            NameTypes = new Dictionary<string, Type>();
        }

        public static void RegistAssemblyModels(Assembly assembly)
        {
            NameTypes = assembly.GetTypes()
                 .Where(x => !x.IsInterface)
                 .Where(x => !x.IsAbstract)
                 .Where(x => x.GetInterface(nameof(IElasticSearchModel)) != null)
                 .ToDictionary(x => x.Name, x => x);
        }

        public static ElasticSearchTypeManager Manager { get; set; } = new ElasticSearchTypeManager();

        public InstanceWithType GetInstanceWithName(string typeName)
        {
            var instanceType = NameTypes[typeName];
            return new InstanceWithType()
            {
                ModelType = instanceType,
                Model = Activator.CreateInstance(instanceType) as IElasticSearchModel
            };
        }

        public class InstanceWithType
        {
            public Type ModelType { get; set; }

            public IElasticSearchModel Model { get; set; }
        }
    }
}
