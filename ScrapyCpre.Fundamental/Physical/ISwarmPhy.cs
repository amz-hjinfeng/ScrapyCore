using System;
using System.Threading.Tasks;
using ScrapyCpre.Fundamental.Models;

namespace ScrapyCpre.Fundamental.Physical
{
    public interface ISwarmPhy
    {
        Task<SwarmPhysicalStatus> GetSwarmPhysicalStatus();

        String Id { get; }
    }
}
