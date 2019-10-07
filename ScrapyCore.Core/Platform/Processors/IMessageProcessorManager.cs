using System.Threading.Tasks;
using ScrapyCore.Core.Platform.Message;

namespace ScrapyCore.Core.Platform.Processors
{
    public interface IMessageProcessorManager
    {
        Task ProcessMessage(PlatformMessage platformMessage);
    }
}