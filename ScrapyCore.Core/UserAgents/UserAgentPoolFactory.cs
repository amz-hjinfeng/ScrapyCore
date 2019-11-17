using System;
using System.Collections.Generic;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.UserAgents
{
    public class UserAgentPoolFactory : IServiceFactory<IUserAgentPool,IUserAgentsConfigure>
    {
        private static UserAgentPoolFactory _factory;
        public static UserAgentPoolFactory Factory
        {

            get
            {
                if (_factory == null)
                    _factory = new UserAgentPoolFactory();
                return _factory;
            }
        }

        private UserAgentPoolFactory()
        {
        }

        public IUserAgentPool GetService(IUserAgentsConfigure configure)
        {
            return new NormalAgentPools(configure);
        }

        public IList<string> GetServiceKeys()
        {
            return new List<string>() { "" };
        }
    }
}
