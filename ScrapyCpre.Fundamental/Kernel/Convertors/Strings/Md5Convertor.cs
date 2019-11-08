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
            contentData.ContentText = contentData.ContentText.ToMD5Hex();
            return contentData;
        }
    }
}
