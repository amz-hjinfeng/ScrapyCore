using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.Processors
{
    public class WorkingProcessor : IMessageProcessor
    {
        public Task ProcessAsync(PlatformMessage platformMessage)
        {
            throw new NotImplementedException();
        }
    }
}
