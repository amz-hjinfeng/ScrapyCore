using log4net;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Storages
{
    public abstract class Storage : IStorage
    {
        protected static ILog logger = LogManager.GetLogger(typeof(Storage));

        protected string Prefix { get; }

        protected Storage(string prefix)
        {
            Prefix = prefix;
        }

        public abstract string StorageName { get; }
        public abstract Task<string> GetStringAsync(string path);
        public abstract string GetString(string path);
    }
}
