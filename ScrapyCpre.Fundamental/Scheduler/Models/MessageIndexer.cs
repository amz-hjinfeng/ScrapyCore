using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Models
{
    public class MessageIndexer
    {
        public string MessageId { get; set; }

        public List<string> SourceJobIds { get; set; }

        public List<string> TransformJobIds { get; set; }

        public List<string> LoadJobIds { get; set; }

        public MessageIndexer()
        {
            SourceJobIds = new List<string>();
            TransformJobIds = new List<string>();
            LoadJobIds = new List<string>();
        }
    }
}
