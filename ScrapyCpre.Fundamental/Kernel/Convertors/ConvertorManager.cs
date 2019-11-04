using Newtonsoft.Json;
using ScrapyCore.Fundamental.Kernel.Convertors.Attributes;
using ScrapyCore.Fundamental.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors
{
    public class ConvertorManager
    {
        public class ConvertorMeta
        {
            public ConvertorAttribute Attribute { get; set; }
            public Type ConvertorType { get; set; }
        }

        public static IReadOnlyDictionary<string, ConvertorMeta> ConvertorMetas { get; set; }

        static ConvertorManager()
        {
            ConvertorMetas = Assembly.GetAssembly(typeof(ConvertorManager))
                .GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsInterface)
                .Select(x =>
                new
                {
                    Attribute = x.GetCustomAttribute<ConvertorAttribute>(),
                    Type = x
                })
                .Where(x => x.Attribute != null)
                .ToDictionary(x => x.Attribute.Name, x => new ConvertorMeta() { Attribute = x.Attribute, ConvertorType = x.Type });
        }
        public ConvertorManager()
        {

        }

        public IConvertor GetConvertor(ConvertorNavigator convertorNavigator)
        {
            if (ConvertorMetas.ContainsKey(convertorNavigator.Name))
            {
                var meta = ConvertorMetas[convertorNavigator.Name];
                object parameter = null;
                if (meta.Attribute.ConstructorType == null)
                {
                    return Activator.CreateInstance(meta.ConvertorType) as IConvertor;
                }
                else if (meta.Attribute.ConstructorType.IsValueType)
                {
                    parameter = Convert.ChangeType(convertorNavigator.Parameter.ToString(), meta.Attribute.ConstructorType);
                }
                else if (meta.Attribute.ConstructorType == typeof(String))
                {
                    parameter = convertorNavigator.Parameter.ToString();
                }
                else
                {
                    parameter = JsonConvert.DeserializeObject(convertorNavigator.Parameter.ToString(), meta.Attribute.ConstructorType);
                }
                return Activator.CreateInstance(meta.ConvertorType, parameter) as IConvertor;
            }

            return null;
        }


    }
}
