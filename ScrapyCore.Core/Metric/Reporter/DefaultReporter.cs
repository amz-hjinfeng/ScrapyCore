using System.Threading.Tasks;

namespace ScrapyCore.Core.Metric.Reporter
{
    public class DefaultReporter : IMetricReporter
    {
        public Task Report(MetricCollections collections)
        {
            return Task.CompletedTask;
        }
    }
}
