using System;
namespace ScrapyCore.Core.Platform.System
{
    public interface ISystemController
    {
        IMessagePipline MessagePipline { get; }

        IWorkingMessageProcessor WorkingProcessor { get; }

        void Start();

        void Pause();

        void Stop();

        void Terminate();
    }
}
