using System.IO;
using System.Threading.Tasks;
using ScrapyCore.Core.Configure;

namespace ScrapyCore.Core.Storages
{
    public class LocalFileSystemStorage : Storage
    {
        private readonly string prefix;

        public LocalFileSystemStorage(IStorageConfigure configure)
            : this(configure.Prefix)
        { }

        public LocalFileSystemStorage(string prefix) : base(prefix)
        {
            this.prefix = prefix;
        }

        public override string StorageName => "LocalFileSystem";

        public override string GetString(string path)
        {
            return File.ReadAllText(Path.Combine(prefix, path));
        }

        public override Task<string> GetStringAsync(string path)
        {
            return File.ReadAllTextAsync(path);
        }
    }
}
