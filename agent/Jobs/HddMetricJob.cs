using agent.Repository;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace agent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly HddMetricsRepository repository;
        private readonly PerformanceCounter hddCounter;
        public HddMetricJob(HddMetricsRepository repository)
        {
            this.repository = repository;
            hddCounter = new PerformanceCounter("LogicalDisk", "% Free Space", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            long hddFreeSpace = Convert.ToInt64(hddCounter.NextValue());
            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            repository.Add(time, hddFreeSpace);
            return Task.CompletedTask;
        }
    }
}
