using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.Metric.Reporter
{
    public interface IMetricReporter
    {
        Task Report(MetricCollections collections);
    }
}
