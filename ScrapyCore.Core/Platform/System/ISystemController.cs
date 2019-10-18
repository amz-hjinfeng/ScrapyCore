using System;
namespace ScrapyCore.Core.Platform.System
{
    public interface ISystemController
    {
        IMessagePipline MessagePipline { get; }

        void Start();

        void Pause();

        void Stop();
    }
}
