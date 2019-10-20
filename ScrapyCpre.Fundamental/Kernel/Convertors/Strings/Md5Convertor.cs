using System.Security.Cryptography;
using System.Text;
using ScrapyCore.Core.External;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    [Attributes.Convertor(nameof(Md5Convertor), null)]
    public class Md5Convertor : Convertor
    {
        public override ContextData Convert(ContextData contentData)
        {
            MD5 md5 = MD5.Create();
            contentData.ContentText =
                md5.ComputeHash(Encoding.UTF8.GetBytes(contentData.ContentText))
                 .ToHex();
            return contentData;
        }
    }
}
