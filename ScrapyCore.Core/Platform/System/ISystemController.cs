using System;
namespace ScrapyCore.Core.Platform.System
{
    public interface ISystemController
    {
        void Start();

        void Break();

        void DirectMessageCall();

        void Stop();
    }
}
