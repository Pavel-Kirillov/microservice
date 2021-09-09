namespace manager.Repository
{
    public class DotNetMetricsRepository : MetricsRepository
    {
        public DotNetMetricsRepository()
        {
            SetTable("dotnetmetrics");
        }
    }
}

