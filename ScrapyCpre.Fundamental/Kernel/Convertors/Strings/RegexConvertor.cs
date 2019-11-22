using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(RegexConvertor), typeof(string))]
    public class RegexConvertor : Convertor
    {
        Regex RegexPatten { get; }
        public RegexConvertor(string regexPatten)
        {
            RegexPatten = new Regex(regexPatten, RegexOptions.Compiled);
        }

        public override ContextData Convert(ContextData contentData)
        {
            var matches = RegexPatten.Matches(contentData.ContentText);
            List<object> captured = new List<object>();
            for (int i = 0; i < matches.Count; i++)
            {
                try
                {
                    string val = matches[i].Value;
                    captured.Add(val);
                }
                catch (Exception)
                {
                    //TODO: Log Required
                }
            }

            return new ContextData()
            {
                Listing = captured,
            };
        }
    }
}
