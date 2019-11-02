using System;

namespace ScrapyCore.Core.Injection
{
    public interface IInjectionProvider
    {
        object CreateInstance(Type t);
    }
}