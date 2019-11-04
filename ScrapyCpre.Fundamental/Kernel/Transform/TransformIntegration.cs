using Newtonsoft.Json;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Fundamental.Kernel.Convertors;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Fundamental.Kernel.Transform
{
    public class TransformIntegration : IWorkingMessageProcessor
    {
        private readonly IStorage coreStorage;

        ConvertorManager ConvertorManager => new ConvertorManager();

        public TransformIntegration(ICache coreCache, IStorage coreStorage)
        {
            CoreCache = coreCache;
            this.coreStorage = coreStorage;
        }
        public ICache CoreCache { get; }

        public async Task Process(byte[] processMessage)
        {
            KernelMessage kernelMessage = JsonConvert.DeserializeObject<KernelMessage>(Encoding.UTF8.GetString(processMessage));
            TransformEvent transformEvent = await CoreCache.RestoreAsync<TransformEvent>("Transform-" + kernelMessage.JobId);
            TransformDataSet transformDataSet = new TransformDataSet();
            string data = await coreStorage.GetStringAsync(transformEvent.GetFrom);

            foreach (var def in transformEvent.FieldDefinitions)
            {
                ContextData contextData = new ContextData()
                {
                    ContentText = data
                };
                var convertorSquence = def.ConvertorNavigators.Select(x => ConvertorManager.GetConvertor(x));
                foreach (var convertor in convertorSquence)
                {
                    contextData = convertor.Convert(contextData);
                }
                TransformFieldWithValue transformFieldWithValue = new TransformFieldWithValue();
                PackageTransformFieldWithValue(transformFieldWithValue, contextData);
                transformFieldWithValue.Name = def.Name;
                transformFieldWithValue.Title = def.Title;
                transformDataSet.FieldValues[def.Name] = transformFieldWithValue;
            }
            using (Stream serialzedStream = await transformDataSet.SerialzeToStream(transformEvent.ExportAs))
            {
                await coreStorage.WriteStream(
                    serialzedStream,
                    transformEvent.SaveTo);
            }
        }



        private static void PackageTransformFieldWithValue(TransformFieldWithValue transformFieldWithValue, ContextData contextData)
        {
            if (contextData.Listing.Count > 0)
            {
                transformFieldWithValue.Value = contextData.Listing.Select(x => x.ToString()).ToList();
            }
            else
            {
                transformFieldWithValue.Value = new System.Collections.Generic.List<string>() { contextData.ContentText };
            }
        }
    }
}
