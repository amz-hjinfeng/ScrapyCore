using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(ReplaceConvertor), typeof(ConvertorInput))]
    public class ReplaceConvertor : Convertor
    {
        private readonly ConvertorInput convertorInput;

        public ReplaceConvertor(ConvertorInput convertorInput)
        {
            this.convertorInput = convertorInput;
        }

        public override ContentData Convert(ContentData contentData)
        {
            contentData.ContentText =
                contentData.ContentText.Replace(
                    convertorInput.PlaceHolder,
                    convertorInput.ReplaceTarget);
            return contentData;
        }


        public class ConvertorInput
        {
            public string PlaceHolder { get; set; }

            public string ReplaceTarget { get; set; }
        }
    }
}
