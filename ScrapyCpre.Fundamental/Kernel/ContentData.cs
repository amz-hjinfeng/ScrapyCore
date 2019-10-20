using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel
{
    public class ContentData
    {
        public string ContentText { get; set; }

        public List<KeyValuePair<string, string>> Session { get; set; }

        public ContentData()
        {
            Session = new List<KeyValuePair<string, string>>();
            ContentText = string.Empty;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
