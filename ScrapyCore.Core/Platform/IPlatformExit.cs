using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform
{
    public interface IPlatformExit
    {
        Task OutRandom<T>(T obj);
        Task OutTransfer<T>(T obj, string target);
    }
}
