using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using log4net;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.MessageOperation
{
    public abstract class MessageRawOperation : IMessageRawOperation
    {
        protected static ILog logger = LogManager.GetLogger(typeof(MessageRawOperation));

        public abstract Task Push(PlatformMessage platformMessage);

    }
}
