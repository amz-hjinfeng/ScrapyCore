using System;
namespace ScrapyCore.Core.External.Conventor
{
    public class StringToBoolConvertor : IObjectConvertor<bool, string>
    {
        public static StringToBoolConvertor Instance { get; private set; } = new StringToBoolConvertor();

        private StringToBoolConvertor() { }

        public bool Parse(string input)
        {

            if (bool.TryParse(input, out var res))
            {
                return res;
            }
            return false;
        }
    }
}
