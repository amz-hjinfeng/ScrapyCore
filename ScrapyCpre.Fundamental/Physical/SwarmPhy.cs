using System;
using System.Threading.Tasks;
using ScrapyCpre.Fundamental.Models;

namespace ScrapyCpre.Fundamental.Physical
{
    public abstract class SwarmPhy :ISwarmPhy
    {
        public abstract string Id { get; }

        public abstract Task<SwarmPhysicalStatus> GetSwarmPhysicalStatus();
    }
}
