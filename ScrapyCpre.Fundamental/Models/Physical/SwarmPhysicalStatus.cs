using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCpre.Fundamental.Models
{
    public class SwarmPhysicalStatus
    {
        public string PhysicalId { get; set; }

        public string PublicIpAddress { get; set; }

        public string PrivateIpAddress { get; set; }

        public int CPUPercentage { get; set; }

        public int MemoryPercentage { get; set; }
    }
}
