using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Enums
{
    public class FileManualConvertor : ManualConvertor
    {
        private readonly ConvetorInput convetorInput;

        public FileManualConvertor(ConvetorInput convetorInput)
        {
            this.convetorInput = convetorInput;
            if (string.IsNullOrEmpty(convetorInput.Encoding))
                convetorInput.Encoding = "utf-8";
            this.ManualData = File.ReadAllLines(convetorInput.Location, Encoding.GetEncoding(convetorInput.Encoding)).ToList();
        }

        public class ConvetorInput
        {
            public string Encoding { get; set; }

            public string Location { get; set; }
        }
    }
}
