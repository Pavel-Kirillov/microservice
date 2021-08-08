namespace agent.Repository
{
    public class DotNetMetricsRepository : MetricsRepository
    {
        public DotNetMetricsRepository()
        {
            SetTable("dotnetmetrics");
        }
    }
}

