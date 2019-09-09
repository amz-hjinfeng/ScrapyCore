using System;
using System.Collections.Generic;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.UserAgents;
using Xunit;

namespace ScrapyCore.Tests.Core.UserAgents
{
    public class NormalAgentPoolsTests
    {
        private readonly NormalAgentPools normalAgentPools;

        public NormalAgentPoolsTests()
        {
            IUserAgentsConfigure userAgentsConfigure = Moq.Mock.Of<IUserAgentsConfigure>();
            List<Tuple<string, string, string>> tuples = new List<Tuple<string, string, string>>();
            tuples.Add(Tuple.Create("Safari 5.1 - MAC", "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_8; en-us) AppleWebKit/534.50 (KHTML, like Gecko) Version/5.1 Safari/534.50", "Desktop"));
            Moq.Mock.Get(userAgentsConfigure)
                .Setup(x => x.GetUserAgents())
                .Returns(tuples);

            normalAgentPools = new NormalAgentPools(userAgentsConfigure);
        }

        [Fact]
        public void GetAgentsTest()
        {
            var agent = normalAgentPools.GetRandomUserAgent();
            Assert.NotNull(agent);
        }

        [Fact]
        public void NumberOfAgentsTest()
        {
            Assert.NotEqual(0, normalAgentPools.NumberOfAgents);
        }

        [Fact]
        public void GetAgentByNameTest()
        {
            var agent = normalAgentPools.GetUserAgent("Safari 5.1 - MAC");
            Assert.NotNull(agent);
        }

    }
}
