using System.Text;

namespace ScrapyCore.Fundamental.Kernel.Convertors.Strings
{
    public class Base64Convertor : Convertor
    {
        public override ContentData Convert(ContentData contentData)
        {
            contentData.ContentText = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(contentData.ContentText));

            return contentData;
        }
    }
}
