using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Caches
{
    public class DefaultLocker : ICacheLocker
    {
        public void Dispose()
        {

        }

        public Task WithLock()
        {
            return Task.CompletedTask;
        }
    }
}
