using System;
using System.Collections.Generic;
using ScrapyCpre.Fundamental.Models.Behaviour;

namespace ScrapyCpre.Fundamental.Models.Message
{
    public class PlatformMessages
    {
        public Command Command { get; set; }

        public List<MessageRoute> Routes { get; set; }

        public byte[] MessageData { get; set; }
    }
}
