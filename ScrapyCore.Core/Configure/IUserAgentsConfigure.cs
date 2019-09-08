using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Configure
{
    public interface IUserAgentsConfigure
    {
        bool EdgeAutomation { get; }
        List<Tuple<string, string, string>> GetUserAgents();
    }
}

