using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Platform.Commands
{
    public enum CommandTransfer
    {
        /// <summary>
        /// Means that you need not process the message and trans forward
        /// </summary>
        Forward,
        /// <summary>
        /// Means that all worker could process it.
        /// </summary>
        Random,
        /// <summary>
        /// Means that the worker was specify
        /// </summary>
        SiteToSite
    }
}
