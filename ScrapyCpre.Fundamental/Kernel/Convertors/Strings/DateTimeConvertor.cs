using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(DateTimeConvertor), typeof(string))]
    public class DateTimeConvertor : Convertor
    {
        private readonly ConvertorInput convertorInput;

        public DateTimeConvertor(ConvertorInput convertorInput)
        {
            this.convertorInput = convertorInput;
        }

        public override ContextData Convert(ContextData contentData)
        {
            var dateSourceString = contentData.ContentText;
            DateTime dateTime = DateTime.ParseExact(dateSourceString, convertorInput.InFormat, new CultureInfo(convertorInput.CultureInfo));
            contentData.ContentText = dateTime.ToString(convertorInput.OutFormat);
            return contentData;
        }

        public class ConvertorInput
        {
            public string InFormat { get; set; }

            public string CultureInfo { get; set; }

            public string OutFormat { get; set; }
        }
    }
}
