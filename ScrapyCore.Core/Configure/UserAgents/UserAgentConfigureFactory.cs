using System;
namespace ScrapyCore.Core.Configure.UserAgents
{
    public class UserAgentConfigureFactory :IConfigurationFactory<IUserAgentsConfigure>
    {
        private static UserAgentConfigureFactory _factory;

        public static UserAgentConfigureFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new UserAgentConfigureFactory();
                return _factory;
            }
        }

        private UserAgentConfigureFactory()
        {

        }

        public IUserAgentsConfigure CreateConfigure(IStorage storage, string Path)
        {
            return new UserAgentsConfigure(storage, path:Path);
        }
    }
}
