using manager.Repository;
using System.Net.Http;

namespace manager.Jobs
{
    public class DotNetMetricJob : MetricJob
    {
        public DotNetMetricJob(IHttpClientFactory httpClient)
        {
            metricAddress = "api/metrics/dotnet/heaps";
            this.httpClient = httpClient;
            repositoryMetrics = new DotNetMetricsRepository();
        }
    }
}
