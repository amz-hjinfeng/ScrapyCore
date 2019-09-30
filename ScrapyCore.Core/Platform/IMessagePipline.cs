using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform
{
    public interface IMessagePipline
    {
        Task<IMessageQueueHandler<PlatformMessage>> FetchMessage();
        void PushMessageBySiteToSiteCommand(PlatformMessage platformMessage);
    }
}