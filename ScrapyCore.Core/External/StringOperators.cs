using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ScrapyCore.Core.External
{
    public static class StringOperators
    {
        public static string ToMD5Hex(this string data)
        {
            MD5 md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(data)).ToHex();
        }

        public static string ToMD5Base64(this string data)
        {
            MD5 md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(data)).ToBase64();
        }
    }
}
