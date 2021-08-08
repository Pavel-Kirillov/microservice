using agent.Repository;
using System.Net.Http;

namespace agent.Jobs
{
    public class RamMetricJob : MetricJob
    {
        public RamMetricJob(IHttpClientFactory httpClient)
        {
            metricAddress = "api/metrics/ram/available";
            this.httpClient = httpClient;
            repositoryMetrics = new RamMetricsRepository();
        }
    }
}
