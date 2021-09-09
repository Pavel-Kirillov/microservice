using manager.Repository;
using System.Net.Http;

namespace manager.Jobs
{
    public class HddMetricJob : MetricJob
    {
        public HddMetricJob(IHttpClientFactory httpClient)
        {
            metricAddress = "api/metrics/hdd/left";
            this.httpClient = httpClient;
            repositoryMetrics = new HddMetricsRepository();
        }
    }
}
