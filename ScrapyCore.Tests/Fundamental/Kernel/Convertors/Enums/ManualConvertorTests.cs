using ScrapyCore.Fundamental.Kernel.Convertors.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Enums
{
    public class ManualConvertorTests
    {
        public List<string> initData = new List<string>();

        public ManualConvertorTests()
        {
            initData.Add("1");
            initData.Add("2");
            initData.Add("3");
            initData.Add("4");
            initData.Add("5");
            initData.Add("6");
            initData.Add("7");
            initData.Add("8");
        }

        [Fact]
        public void ConstructorTest()
        {
            ManualConvertor convertor = new ManualConvertor();
            Assert.Empty(convertor.ManualData);
        }

        [Fact]
        public void ConvertTest()
        {
            ManualConvertor convertor = new ManualConvertor();
            var data = convertor.Convert(null);
            Assert.Empty(data.ContentText);

            convertor = new ManualConvertor(initData);
            data = convertor.Convert(null);
            Assert.Empty(data.ContentText);

            Assert.True(convertor.MoveNext());
            data = convertor.Convert(null);
            Assert.Equal("1", data.ContentText);


        }

        [Fact]
        public void ResetTest()
        {
            ManualConvertor convertor = new ManualConvertor(initData);
            Assert.Equal(-1, convertor.Index);
            convertor.MoveNext();
            Assert.Equal(0, convertor.Index);

        }
        [Fact]
        public void IntegrationTest()
        {
            ManualConvertor convertor = new ManualConvertor(initData);
            var index = 0;
            while (convertor.MoveNext())
            {
                var data = convertor.Convert(null);
                Assert.Equal(initData[index], data.ContentText);
                index++;
            }
        }




    }
}
