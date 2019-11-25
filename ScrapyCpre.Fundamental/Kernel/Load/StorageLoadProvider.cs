using ScrapyCore.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using ScrapyCore.Core.External;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class StorageLoadProvider : LoadProvider
    {
        private readonly IStorage storage;

        public StorageLoadProvider(IStorage storage)
        {
            this.storage = storage;
        }

        public override async Task Load(Stream content, LoadContext ldContext)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await content.CopyToAsync(memoryStream);
                var hashHex = memoryStream.ToArray().ToMD5Hash().ToHex();
                string fileName = ldContext.Parameter.ToString();
                fileName = ReplateHashPlaceHolder(hashHex, fileName);
                fileName = ReplaceDateTime(fileName);
                memoryStream.Seek(0, SeekOrigin.Begin);
                await storage.WriteStream(memoryStream, fileName);
            }
        }

        private string ReplateHashPlaceHolder(string hash, string parameter)
        {
            return parameter.Replace("{hash}", hash);
        }

        private string ReplaceDateTime(string parameter)
        {
            return parameter.Replace("{date-time}", DateTime.Now.ToString(".yyyy-MM-dd"));
        }



    }
}
