using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.StringTables
{
    [Attributes.Convertor(nameof(DistinctConventor), null)]
    public class DistinctConventor : Convertor
    {
        public DistinctConventor()
        {

        }

        public override ContextData Convert(ContextData contentData)
        {
            return new ContextData()
            {
                Listing = contentData.Listing.Distinct().ToList()
            };
        }
    }
}
