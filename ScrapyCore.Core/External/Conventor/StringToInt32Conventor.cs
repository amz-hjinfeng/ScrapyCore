using System;
namespace ScrapyCore.Core.External.Conventor
{
    public class StringToInt32Conventor :IObjectConvertor<int, string>
    {
        public static IObjectConvertor<int,string> Instance { get; private set; } = new StringToInt32Conventor();

        private StringToInt32Conventor()
        {
        }

        public int Parse(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            return 0;
        }
    }
}
