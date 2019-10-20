using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Enums
{
    public class ManualConvertor : EnumeratorConvertor
    {
        public List<string> ManualData { get; set; }
        public ManualConvertor()
        {
            ManualData = new List<string>();
        }

        public ManualConvertor(List<string> manualData)
        {
            this.ManualData = manualData;
        }

        public override string Current => Index == -1 ? "" : ManualData[Index];

        public int Index { get; private set; } = -1;

        public override ContentData Convert(ContentData contentData)
        {
            return new ContentData { ContentText = Current };
        }

        public override bool MoveNext()
        {
            Index += 1;

            if (Index < ManualData.Count)
            {
                return true;
            }
            return false;
        }

        public override void Reset()
        {
            Index = -1;
        }
    }
}
