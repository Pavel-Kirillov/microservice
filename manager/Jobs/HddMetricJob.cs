using agent.Repository;
using System.Net.Http;

namespace agent.Jobs
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
