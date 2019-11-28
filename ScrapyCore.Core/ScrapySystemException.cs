using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core
{
    public class ScrapySystemException : Exception
    {
        public const int SystemNeedToShutDown = 0;
        public const int Log = 2;
        public const int SystemPause = 1;

        public int Action { get; set; }
        public ScrapySystemException(string message) : base(message)
        {

        }
    }
}
