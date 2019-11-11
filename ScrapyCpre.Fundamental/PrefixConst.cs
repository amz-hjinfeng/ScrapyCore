using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Fundamental
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

        public const string SOURCE_TRANSFOR_MAP = "s-t-m-";

        public const string TRANSFORM_LOAD_MAP = "t-l-m-";


        /// <summary>
        /// Source status
        /// </summary>
        public const string SOURCE_STATUS = "s-status-";
        /// <summary>
        /// Target status 
        /// </summary>
        public const string TRANSFORM_STATUS = "t-status-";
        /// <summary>
        /// Load status
        /// </summary>
        public const string LOAD_STATUS = "l-status-";

        /// <summary>
        /// Source meta store key prefix
        /// </summary>
        public const string SOURCE_META = "src-";
        /// <summary>
        /// Transform meta store key prefix
        /// </summary>
        public const string TRANSFORM_META = "trans-";

        /// <summary>
        /// Load meta store key prefix
        /// </summary>
        public const string LOAD_META = "lod-";
    }
}
