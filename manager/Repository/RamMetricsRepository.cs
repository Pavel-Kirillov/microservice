﻿namespace agent.Repository
{
    public class RamMetricsRepository : MetricsRepository
    {
        public RamMetricsRepository()
        {
            SetTable("rammetrics");
        }
    }
}

