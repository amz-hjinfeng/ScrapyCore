using log4net;
using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Consts;
using ScrapyCore.Core.Platform;
using ScrapyCore.Fundamental.Scheduler;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Load
{
    public class LoadIntegration : IWorkingMessageProcessor
    {
        private static ILog logger = LogManager.GetLogger(LogConst.SCRAPY_FUNDAMENTAL, nameof(LoadIntegration));
        private readonly IStorage dataStorage;

        public LoadProviderManager LoadProviderManager { get; }

        public LoadIntegration(ICache coreCache, IStorage dataStorage, LoadProviderManager loadProviderManager)
        {
            CoreCache = coreCache;
            this.dataStorage = dataStorage;
            LoadProviderManager = loadProviderManager;
        }

        public ICache CoreCache { get; }

        public async Task Process(byte[] processMessage, IPlatformExit platformExit)
        {
            KernelMessage kernelMessage = JsonConvert.DeserializeObject<KernelMessage>(Encoding.UTF8.GetString(processMessage));
            LoadEvent loadEvent = await CoreCache.RestoreAsync<LoadEvent>(PrefixConst.LOAD_META + kernelMessage.JobId);
            if (loadEvent != null)
            {
                var loadProviderNavs = loadEvent.LoadProviders.ToDictionary(x => x.Name, x => x);
                foreach (var dataProvider in loadEvent.Data)
                {
                    var loadProviderNav = loadProviderNavs[dataProvider.Provider.Name];
                    ILoadProvider loadProvider = this.LoadProviderManager.GetLoadProvider(loadProviderNav.Type, loadProviderNav.Name, loadProviderNav.Context.ToString());
                    try
                    {
                        await dataStorage.ReadAsStream(dataProvider.DataPacket, s =>
                         {
                             return loadProvider.Load(s,
                                 new LoadContext()
                                 {
                                     Parameter = dataProvider.Provider.Parameter,
                                     PlatformModel = new PlatformModel()
                                     {
                                         CoreCache = CoreCache,
                                         PlatformExit = platformExit
                                     },
                                     LoadEvent = loadEvent
                                 });
                         });
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            else
            {
                logger.Warn("Received no-meta id:" + PrefixConst.LOAD_META + kernelMessage.JobId);
            }

        }
    }
}
