using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(SelectorConvertor), typeof(int))]
    public class SelectorConvertor : Convertor
    {
        public SelectorConvertor(int index)
        {
            this.SelectIndex = index;
        }

        public int SelectIndex { get; set; }

        public override ContentData Convert(ContentData contentData)
        {
            return new ContentData()
            {
                ContentText = contentData.Listing[SelectIndex].ToString()
            };
        }
    }
}
