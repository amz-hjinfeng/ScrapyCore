using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(SplitConvertor), typeof(ConvertorInput))]
    public class SplitConvertor : Convertor
    {
        private readonly ConvertorInput convertorInput;

        public SplitConvertor(ConvertorInput convertorInput)
        {
            this.convertorInput = convertorInput;
        }

        public override ContentData Convert(ContentData contentData)
        {
            ContentData ctData = new ContentData();
            string[] splitedStrings = contentData.ContentText.Split(convertorInput.Spliter);
            if (convertorInput.Indexers != null && convertorInput.Indexers.Length > 0)
            {
                foreach (var indexer in convertorInput.Indexers)
                {
                    ctData.Listing.Add(splitedStrings[indexer]);
                }
            }
            return ctData;
        }

        public class ConvertorInput
        {
            public string Spliter { get; set; }

            public int[] Indexers { get; set; }
        }
    }
}
