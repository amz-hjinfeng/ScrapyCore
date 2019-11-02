using log4net;
using System.IO;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Storages
{
    public abstract class Storage : IStorage
    {
        protected static ILog logger = LogManager.GetLogger("Scrapy-Repo", typeof(Storage));

        protected string Prefix { get; }

        protected Storage(string prefix)
        {
            Prefix = prefix;
        }

        public abstract string StorageName { get; }
        public abstract Task<string> GetStringAsync(string path);
        public abstract string GetString(string path);
        public abstract Task WriteStream(Stream stream, string path);
        public abstract Task WriteBytes(byte[] byteArray, string path);
    }
}
