using ScrapyCore.Core.Configure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ScrapyCore.Core.External;

namespace ScrapyCore.Core.UserAgents
{
    public class NormalAgentPools : IUserAgentPool
    {
        private Dictionary<string, UserAgent> userAgents;

        public NormalAgentPools(IUserAgentsConfigure userAgentsConfigure)
        {
            if (userAgentsConfigure == null) throw new ArgumentNullException(nameof(userAgentsConfigure), "This parameter could not be nullable.");
            userAgents = new Dictionary<string, UserAgent>();
            foreach (var kv in userAgentsConfigure.GetUserAgents())
            {
                userAgents[kv.Item1] = new UserAgent()
                {
                    AgentString = kv.Item1,
                    Name = kv.Item2,
                    ClientType = Enum.Parse<ClientType>(kv.Item3)
                };
            }
        }

        public int NumberOfAgents => userAgents.Count;

        public UserAgent GetRandomUserAgent()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int randomIndex = random.Next(0, userAgents.Count - 1);
            return userAgents.ElementAt(randomIndex).Value;
        }

        public UserAgent GetUserAgent(string name)
        {
            return userAgents.DefaultValue(name);
        }
    }
}
