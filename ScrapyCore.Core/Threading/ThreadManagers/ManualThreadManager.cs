using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ScrapyCore.Core.Threading.ThreadManagers
{
    /// <summary>
    /// A ThreadManager implementation that will use real Threads to handle concurrency.
    /// </summary>
    [Serializable]
    public class ManualThreadManager : ThreadManager
    {
        public ManualThreadManager(int maxThreads)
            : base(maxThreads)
        {
        }

        protected override void RunActionOnDedicatedThread(Action action)
        {
            new Thread(() => RunAction(action, true)).Start();
        }
    }
}
