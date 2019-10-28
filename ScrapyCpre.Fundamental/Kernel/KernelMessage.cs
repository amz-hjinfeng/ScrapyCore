using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Kernel
{
    public class KernelMessage
    {
        /// <summary>
        /// Message id should  to a total submit.  for example, submit a working item that contain
        /// fetch the hold website.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Message Name that should be a total submit name.
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// Job Id should map to a spliting working item.
        /// </summary>
        public string JobId { get; set; }
    }
}
