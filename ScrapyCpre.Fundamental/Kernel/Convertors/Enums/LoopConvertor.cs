using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Enums
{
    [Attributes.Convertor(nameof(LoopConvertor), typeof(ConvertorInput))]
    public class LoopConvertor : EnumeratorConvertor
    {
        private readonly ConvertorInput convertorInput;
        public ConvertorInput Copy { get; set; }


        public LoopConvertor(ConvertorInput convertorInput)
        {
            this.convertorInput = convertorInput;
            this.Copy = new ConvertorInput(this.convertorInput);
        }


        public override string Current => Copy.Start.ToString();

        public override ContentData Convert(ContentData contentData)
        {
            return new ContentData()
            {
                ContentText =
                contentData.ContentText.Replace(
                    Copy.Placeholder,
                    Current,
                    StringComparison.CurrentCulture)
            };
        }

        public override bool MoveNext()
        {
            Copy.Start = Copy.Start + Copy.Delta;
            return Copy.Delta < 0 ? Copy.Start >= Copy.End : Copy.Start <= Copy.End;
        }

        public override void Reset()
        {
            this.Copy = new ConvertorInput(convertorInput);
        }

        public class ConvertorInput
        {
            public ConvertorInput(ConvertorInput major)
            {
                this.Start = major.Start;
                this.End = major.End;
                this.Delta = major.Delta;
                this.Placeholder = major.Placeholder;
            }

            public ConvertorInput()
            {

            }

            public string Placeholder { get; set; }

            public int Start { get; set; }

            public int End { get; set; }

            public int Delta { get; set; }

        }
    }
}
