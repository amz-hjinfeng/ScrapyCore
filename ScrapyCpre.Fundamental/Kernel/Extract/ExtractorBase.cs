using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public abstract class ExtractorBase : IExtractor
    {
        public static ILog Logger => LogManager.GetLogger("Scrapy-Repo", typeof(ExtractorBase));
        public abstract Task ExtractTarget(string paramter, string path);
    }
}
