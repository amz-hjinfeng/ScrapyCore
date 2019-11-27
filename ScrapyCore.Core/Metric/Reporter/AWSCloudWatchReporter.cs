using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

namespace ScrapyCore.Core.Metric.Reporter
{
    public class AWSCloudWatchReporter : IMetricReporter
    {
        private readonly AmazonCloudWatchClient amazonCloudWatchClient;

        public AWSCloudWatchReporter()
        {
            amazonCloudWatchClient = new AmazonCloudWatchClient();
        }

        public Task Report(MetricCollections collections)
        {
            return amazonCloudWatchClient.PutMetricDataAsync(new PutMetricDataRequest()
            {
                Namespace = "HOS",
                MetricData = collections.Metrics.Values.Select(x => new MetricDatum()
                {
                    Unit = StandardUnit.Count,
                    MetricName = x.MetricName,
                    Value = x.GetCurrentReset()
                }).ToList()
            });
        }
    }
}
