using System;
namespace ScrapyCpre.Fundamental.Attributes
{
    [AttributeUsage( AttributeTargets.Parameter, AllowMultiple = false, Inherited =false)]
    public class IdAttribute :Attribute
    {
        public IdAttribute()
        {
        }
    }
}
