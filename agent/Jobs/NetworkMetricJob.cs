using agent.Repository;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace agent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly NetworkMetricsRepository repository;
        public NetworkMetricJob(NetworkMetricsRepository repository)
        {
            this.repository = repository;
            
        }
        public Task Execute(IJobExecutionContext context)
        {
            PerformanceCounter networkCounter = new PerformanceCounter("Network Interface", "Bytes Total/sec");
            PerformanceCounterCategory networkCounterCategory = new PerformanceCounterCategory("Network Interface");
            string[] instanceName = networkCounterCategory.GetInstanceNames();
            long networkByteTotal = 0;
            for (int i = 0; i < instanceName.Length; i++)
            {
                networkCounter.InstanceName = instanceName[i];
                networkByteTotal += Convert.ToInt64(networkCounter.RawValue);
            }
            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            long networkByteTotalTmp = networkByteTotal;
            networkByteTotal -= Startup.valueForNetwork;
            Startup.valueForNetwork = networkByteTotalTmp;
            repository.Add(time, networkByteTotal);
            return Task.CompletedTask;
        }
    }
}
