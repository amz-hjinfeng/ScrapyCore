using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScrapyCore.Core.Storages;
using Xunit;

namespace ScrapyCore.Tests.Core.Storages
{
    public class LocalFileSystemStorageTests
    {
        const string expect = "This is a local file system mock data.";
        LocalFileSystemStorage fileSystemStorage;

        public LocalFileSystemStorageTests()
        {
            string location = Assembly.GetCallingAssembly().Location;
            fileSystemStorage = new LocalFileSystemStorage(Path.GetDirectoryName(location));
        }

        [Fact]
        public void GetStringTest()
        {
            var testStr = fileSystemStorage.GetString("MockData/Core/Storage/localfsmockdata.txt");
            Assert.Equal(expect, testStr);
        }

        [Fact]
        public async Task  GetStringAsyncTest()
        {
            var testStr = await fileSystemStorage.GetStringAsync("MockData/Core/Storage/localfsmockdata.txt");
            Assert.Equal(expect,testStr);
        }

    }
}
