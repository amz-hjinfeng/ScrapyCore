namespace ScrapyCore.Core.Metric
{
    public interface IMetricCollector
    {
        string MetricName { get; set; }

        void Increase(long value);

        double GetCurrentReset();
    }
}
