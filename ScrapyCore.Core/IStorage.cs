using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface IStorage
    {
        string StorageName { get; }

        Task<string> GetStringAsync(string path);

        string GetString(string path);

    }
}
