using ScrapyCore.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class StorageLoadProvider : LoadProvider
    {
        private readonly IStorage storage;

        public StorageLoadProvider(IStorage storage)
        {
            this.storage = storage;
        }

        public override Task Load(Stream content, object paramter)
        {
            return storage.WriteStream(content, paramter.ToString());
        }
    }
}
