using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Caches
{
    public interface ICacheLocker : IDisposable
    {
        Task WithLock();
    }
}
