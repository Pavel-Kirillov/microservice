namespace manager.Repository
{
    public class HddMetricsRepository : MetricsRepository
    {
        public HddMetricsRepository()
        {
            SetTable("hddmetrics");
        }
    }
}

