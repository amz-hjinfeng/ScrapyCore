using System;

namespace ScrapyCore.Core
{
    public interface IThreadManager : IDisposable
    {
        int MaxThreads { get; }
        void DoWork(Action action);

        void AbortAll();
    }
}