using manager.Repository;
using System.Net.Http;

namespace manager.Jobs
{
    public class CpuMetricJob : MetricJob
    {
        public CpuMetricJob(IHttpClientFactory httpClient)
        {
            metricAddress = "api/metrics/cpu";
            this.httpClient = httpClient;
            repositoryMetrics = new CpuMetricsRepository();
        }
    }
}
