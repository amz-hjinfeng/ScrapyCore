using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class WebState
    {
        public string Url { get; set; }

        public string JobId { get; set; }

        public int Depth { get; set; }

        public bool IsCompleted { get; set; }

        public Dictionary<string, WebState> Children { get; set; }

        public WebState()
        {
            Children = new Dictionary<string, WebState>();
        }
    }
}
