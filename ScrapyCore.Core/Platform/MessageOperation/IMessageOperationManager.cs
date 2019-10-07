using ScrapyCore.Core.Platform.Commands;

namespace ScrapyCore.Core.Platform.MessageOperation
{
    public interface IMessageOperationManager
    {
        IMessageRawOperation GetRawOperation(CommandTransfer transfer);
    }
}