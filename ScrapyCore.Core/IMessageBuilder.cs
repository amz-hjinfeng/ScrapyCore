using ScrapyCore.Core.Platform.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface IMessageBuilder
    {
        string BuilderName { get; }
        Task<IMessageBuilder> NextBuilder(PlatformMessage plaformMessage);

    }
}
