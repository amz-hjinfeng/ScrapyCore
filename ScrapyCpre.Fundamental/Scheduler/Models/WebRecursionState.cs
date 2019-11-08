using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler.Models
{
    public class WebRecursionState
    {
        public string MessageId { get; set; }
        public int Depth { get; set; }
        public List<WebState> States { get; set; }

        public WebRecursionState()
        {
            States = new List<WebState>();
        }
    }
}
