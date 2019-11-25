using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Platform.Processors.Model
{
    public class HeartBeatModel
    {
        public string Id { get; set; }

        public DateTime SentTime { get; set; }

        public string ChannelId { get; set; }

        public string Model { get; set; }

        public object External { get; set; }
    }
}
