using agent.Repository;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace agent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly DotNetMetricsRepository repository;
        private readonly PerformanceCounter dotnetCounter;
        public DotNetMetricJob(DotNetMetricsRepository repository)
        {
            this.repository = repository;
            dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }
        public Task Execute(IJobExecutionContext context)
        {
            long dotnetHeaps = Convert.ToInt64(dotnetCounter.NextValue());
            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            repository.Add(time, dotnetHeaps);
            return Task.CompletedTask;
        }
    }
}
