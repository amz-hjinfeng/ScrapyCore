using ScrapyCore.Fundamental.Kernel;
using ScrapyCore.Fundamental.Kernel.Convertors.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Convertors.Enums
{
    public class LoopConvertorTests
    {
        public LoopConvertor.ConvertorInput convertorInput;
        public LoopConvertorTests()
        {
            convertorInput = new LoopConvertor.ConvertorInput()
            {
                Delta = 1,
                End = 100,
                Start = 1,
                Placeholder = "{PAGE}"
            };
        }
        [Fact]
        public void ConstructorTest()
        {
            LoopConvertor loopConvertor = new LoopConvertor(convertorInput);
            Assert.NotEqual(loopConvertor.Copy, convertorInput);
        }

        [Fact]
        public void ConvertTest()
        {
            LoopConvertor loopConvertor = new LoopConvertor(convertorInput);
            ContextData contentData = new ContextData()
            {
                ContentText = "Hello!{PAGE}"
            };
            int i = 1;
            do
            {
                var data = loopConvertor.Convert(contentData);
                Assert.Equal("Hello!" + i, data.ContentText);
                i++;
            } while (loopConvertor.MoveNext());
        }

        [Fact]
        public void MoveNextTest()
        {
            LoopConvertor loopConvertor = new LoopConvertor(convertorInput);
            Assert.True(loopConvertor.MoveNext());

            loopConvertor = new LoopConvertor(new LoopConvertor.ConvertorInput()
            {
                Start = 100,
                Delta = 1,
                End = 10,
                Placeholder = "aaaa"
            });
            Assert.False(loopConvertor.MoveNext());
        }

        [Fact]
        public void ResetTest()
        {
            LoopConvertor loopConvertor = new LoopConvertor(convertorInput);
            Assert.Equal(1, loopConvertor.Copy.Start);
            loopConvertor.MoveNext();
            Assert.Equal(2, loopConvertor.Copy.Start);
            loopConvertor.Reset();
            Assert.Equal(1, loopConvertor.Copy.Start);
        }

        
    }
}
