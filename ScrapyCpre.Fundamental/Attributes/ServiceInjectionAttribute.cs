using System;
namespace ScrapyCpre.Fundamental.Attributes
{
    [AttributeUsage( AttributeTargets.Parameter,AllowMultiple =false,Inherited =false)]
    public class ServiceInjectionAttribute :Attribute
    {
        public ServiceInjectionAttribute(String name)
        {
            Name = name;
        }

        public string Name { get; set; }
       
    }
}
