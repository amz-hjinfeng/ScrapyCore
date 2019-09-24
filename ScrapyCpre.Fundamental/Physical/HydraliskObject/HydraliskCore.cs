using System;
using System.Threading.Tasks;
using ScrapyCore.Core;
using ScrapyCpre.Fundamental.Attributes;
using ScrapyCpre.Fundamental.Models;

namespace ScrapyCpre.Fundamental.Physical.HydraliskObject
{
    public class HydraliskCore : SwarmPhy, IHydraliskPhy
    {
        private readonly ICache cache;
        private readonly Guid guid;
        private readonly string id;

        public HydraliskCore(
            [ServiceInjection("default-cach")]  ICache cache,
            [Id] string id
            )
        {
            this.id = id;
            this.cache = cache;
        }

        public override string Id => id;

        public override Task<SwarmPhysicalStatus> GetSwarmPhysicalStatus()
        {
            return cache.RestoreAsync<SwarmPhysicalStatus>(Id);
        }

    }
}
