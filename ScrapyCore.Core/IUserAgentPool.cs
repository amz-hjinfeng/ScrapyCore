using ScrapyCore.Core.UserAgents;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core
{
    public interface IUserAgentPool
    {
        UserAgent GetUserAgent(string name);

        UserAgent GetRandomUserAgent();

        int NumberOfAgents { get; }
    }
}
