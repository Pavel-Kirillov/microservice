namespace manager.Repository
{
    public class CpuMetricsRepository : MetricsRepository
    {
        public CpuMetricsRepository()
        {
            SetTable("cpumetrics");
        }
    }
}

