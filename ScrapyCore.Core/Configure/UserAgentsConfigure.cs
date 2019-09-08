using Newtonsoft.Json;
using ScrapyCore.Core.Consts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Configure
{
    public class UserAgentsConfigure : IUserAgentsConfigure
    {
        private List<Tuple<string, string, string>> tuples;
        private readonly IStorage storage;
        public UserAgentsConfigure(IStorage storage)
        {
            this.storage = storage;
            tuples = new List<Tuple<string, string, string>>();

            var model = JsonConvert.DeserializeObject<Model>(storage.GetString(PathConstants.UserAgentPath));
            EdgeAutomation = model.EdgeAutomation;
            for (int i = 0; i < model.UserAgents.Length; i++)
            {
                var userAgent = model.UserAgents[i];
                tuples.Add(Tuple.Create(userAgent[0], userAgent[1], userAgent[2]));
            }
        }

        public bool EdgeAutomation { get; }

        public List<Tuple<string, string, string>> GetUserAgents() => tuples;


        private class Model
        {
            public bool EdgeAutomation { get; set; }

            public string[][] UserAgents { get; set; }
        }
    }
}
