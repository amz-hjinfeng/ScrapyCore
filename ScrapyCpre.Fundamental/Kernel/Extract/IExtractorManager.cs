namespace ScrapyCore.Fundamental.Kernel.Extract
{
    public interface IExtractorManager
    {
        IExtractor GetExtrator(string type);
    }
}