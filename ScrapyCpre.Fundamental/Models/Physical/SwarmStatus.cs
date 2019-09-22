using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCpre.Fundamental.Models
{
    public class SwarmStatus
    {
        public Guid Id { get; set; }       

        public SwarmTypes SwarmType { get; set; }

        public DateTime LastUpdated { get; set; }

        public SwarmPhysicalStatus PhysicalStatus { get; set; }
    }
}
