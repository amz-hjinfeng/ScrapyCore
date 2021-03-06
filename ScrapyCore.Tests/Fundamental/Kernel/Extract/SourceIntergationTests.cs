﻿using Moq;
using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Storages;
using ScrapyCore.Fundamental.Kernel.Extract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScrapyCore.Tests.Fundamental.Kernel.Extract
{
    public class SourceIntergationTests
    {
        ICache cache;
        IExtractorManager extractorManager;
        IPlatformExit platformExit;
        string httpSourceDemoString;
        ScrapySource scrapySource;
        public SourceIntergationTests()
        {
            var storage = StorageFactory.Factory.GetLocalStorage(ConstVariable.ApplicationPath);
            httpSourceDemoString = storage.GetString("MockData/Fundamental/Extract/httpsourcedemo.json");
            scrapySource = JsonConvert.DeserializeObject<ScrapySource>(httpSourceDemoString);
            cache = Mock.Of<ICache>();
            extractorManager = Mock.Of<IExtractorManager>();
            var extractor = Mock.Of<IExtractor>();
            platformExit = Mock.Of<IPlatformExit>();



            Mock.Get(cache)
                .Setup(x => x.RestoreAsync<ScrapySource>(It.IsAny<string>()))
                .Returns(Task.FromResult(scrapySource));

            Mock.Get(cache)
                .Setup(x => x.RestoreAsync<List<string>>(It.IsAny<string>()))
                .Returns(Task.FromResult(new List<string>()
                {
                    "a","b"
                }));

            Mock.Get(extractorManager)
                .Setup(x => x.GetExtrator(It.IsAny<string>()))
                .Returns(extractor);

            Mock.Get(extractor).Setup(x => x.ExtractTarget(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) =>
                {
                    Assert.Equal(scrapySource.Source.Parameters.ToString(), a);
                    Assert.Equal(scrapySource.SaveTo, b);
                    return Task.CompletedTask;
                });
            Mock.Get(platformExit).Setup(x => x.OutRandom(It.IsAny<PlatformMessage>()))
                .Returns(Task.CompletedTask);


        }

        [Fact]
        public async Task ProcessTest()
        {
            SourceIntergation sourceIntergation = new SourceIntergation(cache, extractorManager);
            await sourceIntergation.Process(Encoding.UTF8.GetBytes(httpSourceDemoString), platformExit);

        }
    }
}
