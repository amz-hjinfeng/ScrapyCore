using System.Threading;

namespace ScrapyCore.Core.Metric
{
    public class IncreasedMetricCollector : IMetricCollector
    {
        long currentValue;
        public IncreasedMetricCollector(string metricName)
        {
            this.MetricName = metricName;
        }

        public string MetricName { get; set; }

        public double GetCurrentReset()
        {
            long value = Interlocked.Read(ref currentValue);
            Interlocked.Add(ref currentValue, -value);
            return value;
        }

        public void Increase(long value)
        {
            Interlocked.Add(ref currentValue, value);
        }
    }
}
