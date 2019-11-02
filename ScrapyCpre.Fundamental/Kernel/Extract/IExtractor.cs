using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public interface IExtractor
    {
        Task ExtractTarget(string paramter, string path);
    }
}
