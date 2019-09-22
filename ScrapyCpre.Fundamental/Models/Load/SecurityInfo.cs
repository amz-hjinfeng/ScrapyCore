using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCpre.Fundamental.Models.Load
{
    public class SecurityInfo
    {
        public string SecurityName { get; set; }

        public double Pricing { get; set; }

        public DateTime EventTime { get; set; }
    }
}
