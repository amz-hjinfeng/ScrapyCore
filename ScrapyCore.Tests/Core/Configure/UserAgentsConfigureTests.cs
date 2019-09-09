using System;
using System.Collections.Generic;
using System.Text;
using ScrapyCore.Core;
using ScrapyCore.Core.Configure;
using ScrapyCore.Core.Storages;
using Xunit;

namespace ScrapyCore.Tests.Core.Configure
{
    public class UserAgentsConfigureTests
    {
        private UserAgentsConfigure userAgentsConfigure;
        public UserAgentsConfigureTests()
        {
            IStorage storage = new LocalFileSystemStorage(ConstVariable.ApplicationPath);
            userAgentsConfigure = new UserAgentsConfigure(storage,"MockData/Core/Configure/example.json");
        }

        [Fact]
        public void EdgeAutomationTest()
        {
            Assert.True(userAgentsConfigure.EdgeAutomation);
        }

        [Fact]
        public void GetUserAgentsTest()
        {
            var uas = userAgentsConfigure.GetUserAgents();
            Assert.NotNull(uas);
            Assert.NotEmpty(uas);
            Assert.Equal(38, uas.Count);
        }
    }
}
