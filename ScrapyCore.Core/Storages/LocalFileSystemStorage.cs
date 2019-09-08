using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Storages
{
    public class LocalFileSystemStorage : Storage
    {
        public LocalFileSystemStorage(string prefix) : base(prefix)
        { }
        public override string StorageName => "LocalFileSystem";

        public override string GetString(string path)
        {
            return File.ReadAllText(path);
        }

        public override Task<string> GetStringAsync(string path)
        {
            return File.ReadAllTextAsync(path);
        }
    }
}
