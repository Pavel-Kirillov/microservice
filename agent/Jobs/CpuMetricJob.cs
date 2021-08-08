using agent.Repository;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace agent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly CpuMetricsRepository repository;
        private readonly PerformanceCounter cpuCounter;
        public CpuMetricJob(CpuMetricsRepository repository)
        {
            this.repository = repository;
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }
        public Task Execute(IJobExecutionContext context)
        {
            int cpuUsageInPercents = Convert.ToInt32(cpuCounter.NextValue());
            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            repository.Add(time, cpuUsageInPercents);
            return Task.CompletedTask;
        }
    }
}
