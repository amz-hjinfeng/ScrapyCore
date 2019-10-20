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

        public override ContextData Convert(ContextData contentData)
        {
            return new ContextData()
            {
                ContentText = contentData.Listing[SelectIndex].ToString()
            };
        }
    }
}
