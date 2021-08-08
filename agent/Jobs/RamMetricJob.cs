using agent.Repository;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace agent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly RamMetricsRepository repository;
        private readonly PerformanceCounter ramCounter;
       
        public RamMetricJob(RamMetricsRepository repository)
        {
            this.repository = repository;
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            long RamAvailable = Convert.ToInt64(ramCounter.NextValue());
            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            repository.Add(time, RamAvailable);
            return Task.CompletedTask;
        }
    }
}
