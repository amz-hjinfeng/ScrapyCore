using System.Collections.Generic;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Enums
{
    [Attributes.Convertor(nameof(ManualConvertor), typeof(List<string>))]
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

        public override ContextData Convert(ContextData contentData)
        {
            return new ContextData { ContentText = Current };
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
