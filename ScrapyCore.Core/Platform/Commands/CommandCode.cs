using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Platform.Commands
{
    public enum CommandCode
    {
        /// <summary>
        /// Means that this was the sacrifice command ,the work who got it should not process working level command
        /// </summary>
        Sacrifice,
        /// <summary>
        /// Means that this was the working command that the worker should do the work .
        /// </summary>
        Working,
        /// <summary>
        /// Means that this was a heart beat command.
        /// </summary>
        HeartBeat,
        /// <summary>
        /// Means that this was a configure command.
        /// </summary>
        Configure
    }
}
