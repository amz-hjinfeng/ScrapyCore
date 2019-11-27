namespace ScrapyCore.Core.Metric
{
    internal class DefaultMetricCollector : IMetricCollector
    {
        public string MetricName { get; set; } = "DefaultMetric";

        public double GetCurrentReset()
        {
            return 0;
        }

        public void Increase(long value)
        {

        }
    }
}