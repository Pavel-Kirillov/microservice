namespace manager.Repository
{
    public class NetworkMetricsRepository : MetricsRepository
    {
        public NetworkMetricsRepository()
        {
            SetTable("networkmetrics");
        }
    }
}

