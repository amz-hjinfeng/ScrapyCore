using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScrapyCore.Core.Injection
{
    public class InjectionProvider : IInjectionProvider
    {
        private Dictionary<Type, Func<string, object>> TypeMapper { get; }
        public InjectionProvider(Bootstrap bootstrap)
        {
            Bootstrap = bootstrap;
            TypeMapper = new Dictionary<Type, Func<string, object>>()
            {
                {
                    typeof(IStorage),
                    (n)=> bootstrap.GetStorageFromVariableSet(n)
                },
                {
                    typeof(IMessageQueue),
                    n=> bootstrap.GetMessageQueueFromVariableSet(n)
                },
                {
                    typeof(IUserAgentPool),
                    n=> bootstrap.Provisioning.UseragentPools[n]
                },
                {
                    typeof(ICache)   ,
                    n=> bootstrap.Provisioning.Caches[n]
                }
             };
        }

        public Bootstrap Bootstrap { get; }

        public object CreateInstance(Type t)
        {
            ConstructorInfo constructorInfo = t.GetConstructors()[0];
            var parameters = constructorInfo.GetParameters()
                .Select(p => TypeMapper[p.ParameterType](p.GetCustomAttribute<InjectAttribute>().Name))
                .ToArray();
            return Activator.CreateInstance(t, parameters);
        }
    }
}
