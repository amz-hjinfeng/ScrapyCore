using System;
namespace ScrapyCore.Core.External.Conventor
{
    public interface IObjectConvertor<OUTPUT,INPUT>
    {
        OUTPUT Parse(INPUT input);
    }
}
