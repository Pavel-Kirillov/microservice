namespace agent.Repository
{
    public class NetworkMetricsRepository : MetricsRepository
    {
        public NetworkMetricsRepository()
        {
            SetTable("networkmetrics");
        }
    }
}

