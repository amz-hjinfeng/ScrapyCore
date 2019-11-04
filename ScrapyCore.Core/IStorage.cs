using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core
{
    public interface IStorage
    {
        string StorageName { get; }

        Task<string> GetStringAsync(string path);

        string GetString(string path);

        Task WriteStream(Stream stream, string path);

        Task WriteBytes(byte[] byteArray, string path);

        Task ReadAsStream(string path, Func<Stream, Task> streamUsage);

    }
}
