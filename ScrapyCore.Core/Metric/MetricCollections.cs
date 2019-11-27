using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapyCore.Core.Metric
{
    public class MetricCollections
    {
        public static MetricCollections Default { get; private set; } = new MetricCollections();

        public Dictionary<string, IMetricCollector> Metrics { get; set; }

        public MetricCollections()
        {
            Metrics = new Dictionary<string, IMetricCollector>();
        }

        public IMetricCollector MetricGetMetricCollector(string metricName)
        {
            if (Metrics.ContainsKey(metricName))
            {
                return Metrics[metricName];
            }
            return new DefaultMetricCollector();
        }

        public void AddMetricCollector(string alisa, IMetricCollector metric)
        {
            Metrics[alisa] = metric;
        }



    }
}
