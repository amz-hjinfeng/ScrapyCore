using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.UserAgents
{
    public class UserAgent
    {
        public string Name { get; set; }

        public string AgentString { get; set; }

        public ClientType ClientType { get; set; }
    }
}
