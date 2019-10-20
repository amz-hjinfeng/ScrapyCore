using ScrapyCore.Fundamental.Kernel.Convertors.Enums;
using ScrapyCore.Fundamental.Kernel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;
using System.Linq;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Enums
{
    public class FileManualConvertorTests
    {
        string dataPath;

        FileManualConvertor.ConvetorInput conventorInput;

        public FileManualConvertorTests()
        {
            dataPath = ConstVariable.ApplicationPath + "/MockData/Fundamental/Conventors/MockFileManualConvertor.txt";
            conventorInput = new FileManualConvertor.ConvetorInput()
            {
                Encoding = "utf-8",
                Location = dataPath
            };
        }

        [Fact]
        public void ResetTest()
        {
            FileManualConvertor convertor = new FileManualConvertor(conventorInput);
            Assert.Equal(-1, convertor.Index);
            convertor.MoveNext();
            Assert.Equal(0, convertor.Index);
        }

        [Fact]
        public void ConvertTest()
        {
            FileManualConvertor convertor = new FileManualConvertor(conventorInput);
            var data = convertor.Convert(null);
            Assert.Empty(data.ContentText);

            convertor = new FileManualConvertor(conventorInput);
            data = convertor.Convert(null);
            Assert.Empty(data.ContentText);

            Assert.True(convertor.MoveNext());
            data = convertor.Convert(null);
            Assert.Equal("1", data.ContentText);


        }

        [Fact]
        public void ConstructorTest()
        {
            FileManualConvertor convertor = new FileManualConvertor(conventorInput);
            Assert.NotEmpty(convertor.ManualData);
        }

        [Fact]
        public void IntegrationTest()
        {
            FileManualConvertor convertor = new FileManualConvertor(conventorInput);
            var index = 0;
            var initData = File.ReadAllLines(conventorInput.Location).ToList();
            while (convertor.MoveNext())
            {
                var data = convertor.Convert(null);
                Assert.Equal(initData[index], data.ContentText);
                index++;
            }
        }
    }
}
