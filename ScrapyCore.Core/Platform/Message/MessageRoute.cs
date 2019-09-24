using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Platform.Message
{
    public class MessageRoute
    {
        public Pricipal Pricipal { get; set; }
        public DateTime DateTimeIn { get; set; }
        public DateTime DateTimeOut { get; set; }

        public MessageRoute(Pricipal pricipal)
        {
            this.Pricipal = pricipal;
            DateTimeIn = DateTime.Now;
        }

        public void Complete()
        {
            DateTimeOut = DateTime.Now;
        }
    }
}
