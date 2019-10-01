using ScrapyCore.Core.Platform.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Platform.Message
{
    public class PlatformMessage
    {
        public Command Command { get; set; }

        public List<MessageRoute> Routes { get; set; }

        public byte[] MessageData { get; set; }

        public Pricipal NextJump { get; set; }
    }
}
