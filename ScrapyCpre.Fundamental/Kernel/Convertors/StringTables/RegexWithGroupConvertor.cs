using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrapyCore.Fundamental.Kernel.Convertors.StringTables
{
    [Attributes.Convertor(nameof(RegexWithGroupConvertor), typeof(ConvertorParameter))]
    public class RegexWithGroupConvertor : Convertor
    {
        Regex RegexPatten { get; }
        int GroupIndex { get; }
        public RegexWithGroupConvertor(ConvertorParameter parameter)
        {
            RegexPatten = new Regex(parameter.RegexPatten, RegexOptions.Compiled);
            GroupIndex = parameter.GroupIndex;
        }


        public override ContextData Convert(ContextData contentData)
        {
            var matches = RegexPatten.Matches(contentData.ContentText);
            List<object> captured = new List<object>();
            for (int i = 0; i < matches.Count; i++)
            {
                try
                {
                    string val = matches[i].Groups[GroupIndex].Value;
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

        public class ConvertorParameter
        {
            public string RegexPatten { get; set; }

            public int GroupIndex { get; set; }
        }
    }
}
