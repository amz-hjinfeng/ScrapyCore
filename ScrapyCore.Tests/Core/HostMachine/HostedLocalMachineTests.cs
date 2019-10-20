using ScrapyCore.Core.HostMachine;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Core.HostMachine
{
    public class HostedLocalMachineTests
    {
        IHostedMachine hostedMachine;
        public HostedLocalMachineTests()
        {
            hostedMachine = new HostedLocalMachine();
        }

        [Fact]
        public void IdTest()
        {
            var id = hostedMachine.Id;
            Assert.True(Guid.TryParse(id, out var guid));

        }

        [Fact]
        public void HostNameTest()
        {
            var hostName = hostedMachine.HostName;
            Assert.NotNull(hostName);
        }

        [Fact]
        public void PrivateIpAddressTest()
        {
            var Ipaddress = hostedMachine.PrivateIpAddress;
            Assert.NotNull(Ipaddress);
        }

        [Fact]
        public void PublicIpAddressTest()
        {
            var Ipaddress = hostedMachine.PublicIpAddress;
            Assert.DoesNotContain("192.168", Ipaddress);
        }

        [Fact]
        public void OperationSystemTest()
        {
            var operationSystem = hostedMachine.OperatingSystem;
            Assert.NotNull(operationSystem);
        }

    }
}
