using ScrapyCore.Core;
using ScrapyCore.Core.Storages;
using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Load;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Load
{
    public class StorageLoadProviderTests
    {
        ILoadProvider loadProvider;
        public StorageLoadProviderTests()
        {
            IStorage storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            loadProvider = new StorageLoadProvider(storage);
        }

        [Fact]
        public async Task LoadTests()
        {
            string fileName = "StorageLoadProviderTests.bin";
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(new byte[] { 0x01, 0x02, 0x03, 0x04 }, 0, 4);
            memoryStream.Seek(0, SeekOrigin.Begin);
            await loadProvider.Load(memoryStream, fileName);
            string writePath = Path.Combine(ConstVariable.ApplicationPath, fileName);
            Assert.True(File.Exists(writePath));
            Assert.Equal(4, File.ReadAllBytes(writePath).Length);
            File.Delete(writePath);
        }
    }
}
