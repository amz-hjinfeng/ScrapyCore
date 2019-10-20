using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Meta
{
    public class DataFieldDef
    {
        public string Name { get; set; }

        public string MetaName { get; set; }

        public bool CanNullable { get; set; }

        public List<ConvertorNavigator> Navigators { get; set; }

        public int Index { get; set; }

        public int Sort { get; set; }

    }
}
