using System;
using System.Collections.Generic;

namespace ScrapyCore.Core.Configure.Storage
{
    public class LocalFileSystemStorageConfigure :IStroageConfigure
    {
        public LocalFileSystemStorageConfigure()
        {

        }

        public string Prefix => throw new NotImplementedException();

        public string StorageType => "LocalFileSystem";

        public Dictionary<string, string> ConfigureDetail => throw new NotImplementedException();
    }
}
