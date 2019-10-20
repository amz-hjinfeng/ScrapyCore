using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConvertorAttribute : Attribute
    {
        public string Name { get; private set; }

        public Type ConstructorType { get; private set; }

        public ConvertorAttribute(string className, Type constructorType)
        {
            this.Name = className.Replace("Convertor", "", StringComparison.CurrentCultureIgnoreCase);
            this.ConstructorType = constructorType;
        }
    }
}
