using log4net;
using ScrapyCore.Core.Threading.ThreadManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ScrapyCore.Core.Threading
{
    [Serializable]
    public abstract class ThreadManager : IThreadManager
    {
        protected static ILog logger = LogManager.GetLogger("Scrapy-Repo", typeof(ThreadManager));
        protected bool _abortAllCalled = false;
        protected int numberOfRunningThread = 0;
        protected ManualResetEvent autoResetEvent = new ManualResetEvent(true);
        protected object locker = new object();
        protected bool isDisposed = false;

        public ThreadManager(int maxThreads)
        {
            if ((maxThreads > 100) || (maxThreads < 1))
                throw new ArgumentException("MaxThreads must be from 1 to 100");

            MaxThreads = maxThreads;
        }

        /// <summary>
        /// Max number of threads to use
        /// </summary>
        public int MaxThreads
        {
            get;
            private set;
        }

        /// <summary>
        /// Will perform the action asynchrously on a seperate thread
        /// </summary>
        public virtual void DoWork(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (_abortAllCalled)
                throw new InvalidOperationException("Cannot call DoWork() after AbortAll() or Dispose() have been called.");

            if (!isDisposed && MaxThreads > 1)
            {
                autoResetEvent.WaitOne();
                lock (locker)
                {
                    numberOfRunningThread++;
                    if (!isDisposed && numberOfRunningThread >= MaxThreads)
                        autoResetEvent.Reset();

                    logger.DebugFormat("Starting another thread, increasing running threads to [{0}].", numberOfRunningThread);
                }
                RunActionOnDedicatedThread(action);
            }
            else
            {
                RunAction(action, false);
            }
        }

        public virtual void AbortAll()
        {
            _abortAllCalled = true;
            numberOfRunningThread = 0;
        }

        public virtual void Dispose()
        {
            AbortAll();
            autoResetEvent.Dispose();
            isDisposed = true;
        }

        public virtual bool HasRunningThreads()
        {
            return numberOfRunningThread > 0;
        }

        protected virtual void RunAction(Action action, bool decrementRunningThreadCountOnCompletion = true)
        {
            try
            {
                action.Invoke();
                logger.Debug("Action completed successfully.");
            }
            catch (OperationCanceledException)
            {
                logger.DebugFormat("Thread cancelled.");
                throw;
            }
            catch (Exception e)
            {
                logger.Error("Error occurred while running action.");
                logger.Error(e);
            }
            finally
            {
                if (decrementRunningThreadCountOnCompletion)
                {
                    lock (locker)
                    {
                        numberOfRunningThread--;
                        logger.DebugFormat("[{0}] threads are running.", numberOfRunningThread);
                        if (!isDisposed && numberOfRunningThread < MaxThreads)
                            autoResetEvent.Set();
                    }
                }
            }
        }

        /// <summary>
        /// Runs the action on a seperate thread
        /// </summary>
        protected abstract void RunActionOnDedicatedThread(Action action);


        public static ThreadManager BuildThreadManager(string managerType, int maxConcurrency)
        {
            switch (managerType)
            {
                case "Task":
                    return new TaskThreadManager(maxConcurrency);
                default:
                    return new ManualThreadManager(maxConcurrency);
            }
        }
    }
}
