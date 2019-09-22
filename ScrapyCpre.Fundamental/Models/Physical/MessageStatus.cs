using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCpre.Fundamental.Models.Physical
{
    public enum MessageStatus
    {
        Queued,
        Processing,
        Complete,
        Timeout
    }
}
