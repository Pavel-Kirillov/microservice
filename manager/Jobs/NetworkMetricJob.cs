using agent.Repository;
using System.Net.Http;

namespace agent.Jobs
{
    public class NetworkMetricJob : MetricJob
    {
        public NetworkMetricJob(IHttpClientFactory httpClient)
        {
            metricAddress = "api/metrics/network";
            this.httpClient = httpClient;
            repositoryMetrics = new NetworkMetricsRepository();
        }
    }
}
