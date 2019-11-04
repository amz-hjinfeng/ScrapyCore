using System;
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
            return File.ReadAllTextAsync(Path.Combine(prefix, path));
        }

        public override async Task ReadAsStream(string path, Func<Stream, Task> streamUsage)
        {
            using (var stream = new FileStream(Path.Combine(prefix, path), FileMode.Open, FileAccess.Read))
            {
                await streamUsage(stream);
            }
        }

        public override async Task WriteBytes(byte[] byteArray, string path)
        {
            using (var fs = File.OpenWrite(Path.Combine(prefix, path)))
            {
                await fs.WriteAsync(byteArray, 0, byteArray.Length);
                await fs.FlushAsync();
                fs.Close();
            }
        }

        public override async Task WriteStream(Stream stream, string path)
        {
            using (var fs = File.OpenWrite(Path.Combine(prefix, path)))
            {
                await stream.CopyToAsync(fs);
                await fs.FlushAsync();
                fs.Close();
            }
        }
    }
}
