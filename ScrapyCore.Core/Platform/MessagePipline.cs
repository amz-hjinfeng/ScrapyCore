using log4net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform
{
    public class MessagePipline : IMessagePipline
    {
        private static ILog logger = LogManager.GetLogger("Scrapy-Repo", typeof(MessagePipline));
        public IMessageEntrance Entrance { get; }
        public IMessageTermination Termination { get; }

        public MessagePipline(IMessageEntrance messageEntrance, IMessageTermination messageTermination)
        {
            Entrance = messageEntrance;
            Termination = messageTermination;
        }
        public async Task Drive()
        {
            logger.Info("Message Pipline Started");
            var messageHandler = await Entrance.FetchMessage();
            if (messageHandler != null)
            {
                var message = messageHandler.MessageObject;
                if (message.Command.CommandCode == Commands.CommandCode.Configure)
                {
                    logger.Info("Recesived the delay command");
                    int delay = BitConverter.ToInt32(message.MessageData);
                    await Task.Delay(delay);
                    logger.Info("Completed the delay command");
                }
                else
                {
                    logger.Info("Message Get:" + message.Command.CommandType.ToString());
                    await Termination.Terminate(message);
                    logger.Info("Message Terminated");
                }
                await messageHandler.Complete();
                logger.Info("Message Pipline Completed");
            }
            else
            {
                logger.Info("Message get nothing from Queue");
                Thread.Sleep(1000);
            }

        }
        public Task Drive(string[] args)
        {
            return Drive();
        }
    }
}
