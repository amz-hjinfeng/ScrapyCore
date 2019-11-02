using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Storages
{
    public class MemoryAsStorage : Storage
    {
        Dictionary<string, byte[]> MemoryData = new Dictionary<string, byte[]>();
        public MemoryAsStorage() : base("/")
        {

        }

        public override string StorageName => "MemoryAsStorage";

        public override string GetString(string path)
        {
            if (MemoryData.ContainsKey(path))
            {
                return Encoding.UTF8.GetString(MemoryData[path]);
            }
            return null;
        }

        public override Task<string> GetStringAsync(string path)
        {
            return Task.FromResult(GetString(path));
        }

        public override Task WriteBytes(byte[] byteArray, string path)
        {
            MemoryData[path] = byteArray;
            return Task.CompletedTask;
        }

        public override Task WriteStream(Stream stream, string path)
        {
            throw new NotImplementedException();
        }
    }
}
