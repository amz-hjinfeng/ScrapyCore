using ScrapyCore.Core.Injection;
using ScrapyCore.Fundamental.Kernel.Extract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using ScrapyCore.Fundamental.Kernel.Extract.Http;

namespace ScrapyCore.Tests.Fundamental.Kernel.Extract
{
    public class ExtractorManagerTests
    {
        private IInjectionProvider injectionProvider;
        public ExtractorManagerTests()
        {
            injectionProvider = Mock.Of<IInjectionProvider>();
            Mock.Get(injectionProvider).Setup(x => x.CreateInstance(It.IsAny<Type>()))
                .Returns((Type x) =>
                {
                    if (typeof(HttpExtractor) == x)
                    {
                        return new HttpExtractor(null, null);
                    }
                    return null;
                });
        }

        [Fact]
        public void StaticConstructorTest()
        {
            var sourceType = ExtractorManager.SourceTypeMapping;
            Assert.NotNull(sourceType);
            Assert.NotEmpty(sourceType);
            Assert.True(sourceType.ContainsKey("Http"));
        }

        [Fact]
        public void GetExtractorTest()
        {
            ExtractorManager extractorManager = new ExtractorManager(injectionProvider);
            IExtractor extractor = extractorManager.GetExtrator("Http");
            Assert.NotNull(extractor);
            Assert.Equal(typeof(HttpExtractor), extractor.GetType());

        }
    }
}
