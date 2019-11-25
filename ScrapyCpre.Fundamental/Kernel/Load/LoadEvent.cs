using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadEvent
    {
        public string MessageId { get; set; }

        public string JobId { get; set; }

        public string SourceId { get; set; }

        public string TransformId { get; set; }

        public LoadProviderNavigator[] LoadProviders { get; set; }

        public LoadDataNavigator[] Data { get; set; }
    }
}
