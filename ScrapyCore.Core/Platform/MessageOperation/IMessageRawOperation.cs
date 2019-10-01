using ScrapyCore.Core.Platform.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform.MessageOperation
{
    public interface IMessageRawOperation
    {
        Task Push(PlatformMessage platformMessage);


    }
}
