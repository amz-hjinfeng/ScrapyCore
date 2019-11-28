using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace ScrapyCore.Core.HostMachine
{
    public class AWSHostingManager : IHostingManager
    {
        public IAmazonEC2 amazonec2;

        public AWSHostingManager()
        {
            amazonec2 = new AmazonEC2Client();
        }

        public Task Terminate(IHostedMachine hostedMachine)
        {
            return amazonec2.TerminateInstancesAsync(new TerminateInstancesRequest()
            {
                InstanceIds = new List<string>()
                 {
                     hostedMachine.Id
                 }
            });
        }
    }
}
