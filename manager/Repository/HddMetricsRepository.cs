﻿namespace agent.Repository
{
    public class HddMetricsRepository : MetricsRepository
    {
        public HddMetricsRepository()
        {
            SetTable("hddmetrics");
        }
    }
}

