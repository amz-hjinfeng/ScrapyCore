using System.Security.Cryptography;
using System.Text;
using ScrapyCore.Core.External;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    public class Md5Convertor : Convertor
    {
        public override ContentData Convert(ContentData contentData)
        {
            MD5 md5 = MD5.Create();
            contentData.ContentText =
                md5.ComputeHash(Encoding.UTF8.GetBytes(contentData.ContentText))
                 .ToHex();
            return contentData;
        }
    }
}
