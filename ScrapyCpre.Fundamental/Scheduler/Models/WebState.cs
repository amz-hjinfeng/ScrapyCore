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

        public List<string> Children { get; set; }
    }
}
