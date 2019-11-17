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
        protected readonly IStorage storage;

        public UserAgentsConfigure(IStorage storage,string path)
        {
            this.storage = storage;
            tuples = new List<Tuple<string, string, string>>();

            var model = JsonConvert.DeserializeObject<Model>(storage.GetString(path));
            EdgeAutomation = model.EdgeAutomation;
            for (int i = 0; i < model.UserAgents.Length; i++)
            {
                var userAgent = model.UserAgents[i];
                tuples.Add(Tuple.Create(userAgent[0], userAgent[1], userAgent[2]));
            }
        }

        public UserAgentsConfigure(IStorage storage)
            :this(storage,PathConstants.UserAgentConfigurePath)
        {

        }

        public bool EdgeAutomation { get; }

        public IDictionary<string, string> ConfigureDetail => throw new NotImplementedException();

        public List<Tuple<string, string, string>> GetUserAgents() => tuples;


        private class Model
        {
            public bool EdgeAutomation { get; set; }

            public string[][] UserAgents { get; set; }
        }
    }
}
