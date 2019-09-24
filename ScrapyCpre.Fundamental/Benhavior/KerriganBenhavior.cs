using System;
using ScrapyCore.Core;
using ScrapyCpre.Fundamental.Attributes;

namespace ScrapyCpre.Fundamental.Benhavior
{
    public class KerriganBenhavior :IKerriganBenhavior
    {
        private readonly ICache cache;
        private readonly IMessageQueue kerriganToHydralisk;
        private readonly IMessageQueue utraliskToKerrigan;
        private readonly IStorage kerriganStorage;
        private readonly ISinkService sinkService;
        private readonly string id;

        public KerriganBenhavior(
                [ServiceInjection("default-cach")]  ICache cache,
                [ServiceInjection("kerrigan-hydralisk")] IMessageQueue kerriganToHydralisk,
                [ServiceInjection("utralisk-kerrigan")] IMessageQueue utraliskToKerrigan,
                [ServiceInjection("kerrigan")]IStorage kerriganStorage,
                [ServiceInjection("Sink")]ISinkService sinkService,
                [Id] string id
            )
        {
            this.cache = cache;
            this.kerriganToHydralisk = kerriganToHydralisk;
            this.utraliskToKerrigan = utraliskToKerrigan;
            this.kerriganStorage = kerriganStorage;
            this.sinkService = sinkService;
            this.id = id;
        }

    }
}
