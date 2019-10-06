using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Platform
{
    /// <summary>
    /// Engine that drive the Message flow.
    /// </summary>
    public interface IMessagePipline
    {
        Task Drive();
        Task Drive(string[] args);
    }
}
