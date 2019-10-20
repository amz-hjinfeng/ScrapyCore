using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel
{
    public class ContextData
    {
        public string ContentText { get; set; }

        public List<KeyValuePair<string, string>> Session { get; set; }

        public List<object> Listing { get; set; }

        public ContextData()
        {
            Session = new List<KeyValuePair<string, string>>();
            Listing = new List<object>();
            ContentText = string.Empty;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
