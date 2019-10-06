using log4net;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform
{
    public class MessagePipline : IMessagePipline
    {
        private static ILog logger = LogManager.GetLogger("Scrapy-Repo", typeof(MessagePipline));
        public IMessageEntrance MessageEntrance { get; }
        public IMessageTermination MessageTermination { get; }
        public MessagePipline(IMessageEntrance messageEntrance, IMessageTermination messageTermination)
        {
            MessageEntrance = messageEntrance;
            MessageTermination = messageTermination;
        }
        public async Task Drive()
        {
            logger.Info("Message Pipline Started");
            var messageHandler = await MessageEntrance.FetchMessage();
            var message = messageHandler.MessageObject;
            logger.Info("Message Get:" + message.Command.CommandType.ToString());
            await MessageTermination.Terminate(message);
            logger.Info("Message Terminated");
            await messageHandler.Complete();
            logger.Info("Message Pipline Completed");
        }
        public Task Drive(string[] args)
        {
            return Drive();
        }
    }
}
