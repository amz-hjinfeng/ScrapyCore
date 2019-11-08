using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental.Scheduler
{
    public class PrefixConst
    {
        /// <summary>
        /// Meta prefix that contain the All ScheduleMessage
        /// </summary>
        public const string MESSAGE_META = "meta-";

        /// <summary>
        /// Message jobs Index
        /// </summary>
        public const string MESSAGE_JOBs = "msg-";

        /// <summary>
        /// Extract to transform list
        /// </summary>
        public const string SOURCE_DOWNFLOW = "etl-";
        /// <summary>
        /// Transofrom to Load list
        /// </summary>
        public const string TRANSFORM_DOWNFLOW = "tll-";
        /// <summary>
        /// Source status
        /// </summary>
        public const string SOURCE_ID = "s-status-";
        /// <summary>
        /// Target status 
        /// </summary>
        public const string TRANSFORM_ID = "t-status-";
        /// <summary>
        /// Load status
        /// </summary>
        public const string LOAD_STATUS = "l-status-";

    }
}
