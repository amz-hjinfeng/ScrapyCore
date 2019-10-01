using System;
namespace ScrapyCore.Core.Platform.System
{
    public interface ISystemController
    {
        void Start();

        void Pause();

        void Stop();
    }
}
