using System;
using System.IO;
using System.Reflection;

namespace ScrapyCore.Tests
{
    public static class ConstVariable
    {
        public static string ApplicationPath { get; }

        static ConstVariable()
        {
            string location = Assembly.GetCallingAssembly().Location;
            ApplicationPath = Path.GetDirectoryName(location);

        }
    }
}
