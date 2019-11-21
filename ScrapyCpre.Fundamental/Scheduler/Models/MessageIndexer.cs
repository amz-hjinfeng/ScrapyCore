using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Models
{
    public class MessageIndexer
    {
        public string MessageId { get; set; }

        public string MessageName { get; set; }

        public DateTime StartTime { get; set; }

        public int SubTask => SourceJobIds.Count + TransformJobIds.Count + LoadJobIds.Count;

        public int Completed =>
            SourceJobIds.Values.Count(x => x != 0) +
            TransformJobIds.Values.Count(x => x != 0) +
            TransformJobIds.Values.Count(x => x != 0);

        public Dictionary<string, int> SourceJobIds { get; set; }

        public Dictionary<string, int> TransformJobIds { get; set; }

        public Dictionary<string, int> LoadJobIds { get; set; }

        public MessageIndexer()
        {
            SourceJobIds = new Dictionary<string, int>();
            TransformJobIds = new Dictionary<string, int>();
            LoadJobIds = new Dictionary<string, int>();
        }
    }
}
