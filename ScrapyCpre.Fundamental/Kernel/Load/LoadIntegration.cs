using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadIntegration : IWorkingMessageProcessor
    {
        private readonly IStorage dataStorage;

        public LoadProviderManager LoadProviderManager { get; }

        public LoadIntegration(ICache coreCache, IStorage dataStorage, LoadProviderManager loadProviderManager)
        {
            CoreCache = coreCache;
            this.dataStorage = dataStorage;
            LoadProviderManager = loadProviderManager;
        }

        public ICache CoreCache { get; }

        public async Task Process(byte[] processMessage)
        {
            KernelMessage kernelMessage = JsonConvert.DeserializeObject<KernelMessage>(Encoding.UTF8.GetString(processMessage));
            LoadEvent loadEvent = await CoreCache.RestoreAsync<LoadEvent>("Load-" + kernelMessage.JobId);
            var loadProviderNavs = loadEvent.LoadProviders.ToDictionary(x => x.Name, x => x);
            foreach (var dataProvider in loadEvent.Data)
            {
                var loadProviderNav = loadProviderNavs[dataProvider.Provider.Name];
                ILoadProvider loadProvider = this.LoadProviderManager.GetLoadProvider(loadProviderNav.Type, loadProviderNav.Name, loadProviderNav.Context.ToString());
                await dataStorage.ReadAsStream(dataProvider.DataPacket, s =>
                 {
                     return loadProvider.Load(s, dataProvider.Provider.Parameter);
                 });
            }
        }
    }
}
