using log4net;
using log4net.Repository;
using System;
using System.IO;
using System.Reflection;

namespace ScrapyCore.Tests
{
    public static class ConstVariable
    {
        public static string ApplicationPath { get; }

        public const string RedisConfigureJson = "MockData/Core/Configure/RedisConfigure.json";

        static ConstVariable()
        {
            string location = Assembly.GetCallingAssembly().Location;
            ApplicationPath = Path.GetDirectoryName(location);
            ILoggerRepository repository = LogManager.CreateRepository("Scrapy-Repo");
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }
    }
}
