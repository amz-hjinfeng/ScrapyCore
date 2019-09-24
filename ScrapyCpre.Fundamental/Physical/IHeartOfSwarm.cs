using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ScrapyCpre.Fundamental.Physical
{
    public interface IHeartOfSwarm
    {
        Task<IEnumerable<IKerriganPhy>> ListKerriganAsync();

        Task<IEnumerable<IHydraliskPhy>> ListHydraliskAsync();

        Task<IEnumerable<IUtralisksPhy>> ListUtraliskAsync();
    }
}
