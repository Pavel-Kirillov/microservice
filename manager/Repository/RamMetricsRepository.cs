namespace manager.Repository
{
    public class RamMetricsRepository : MetricsRepository
    {
        public RamMetricsRepository()
        {
            SetTable("rammetrics");
        }
    }
}

