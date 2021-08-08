namespace agent.Repository
{
    public class CpuMetricsRepository : MetricsRepository
    {
        public CpuMetricsRepository()
        {
            SetTable("cpumetrics");
        }
    }
}

