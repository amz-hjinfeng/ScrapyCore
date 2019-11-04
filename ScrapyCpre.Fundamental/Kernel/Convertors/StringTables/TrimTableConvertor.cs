using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.StringTables
{
    [Attributes.Convertor(nameof(TrimTableConvertor), null)]
    public class TrimTableConvertor : Convertor
    {
        public override ContextData Convert(ContextData contentData)
        {
            for (int i = 0; i < contentData.Listing.Count; i++)
            {
                contentData.Listing[i] = contentData.Listing[i].ToString().Trim();
            }
            return contentData;
        }
    }
}
