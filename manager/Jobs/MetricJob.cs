using agent.Class;
using agent.Repository;
using manager;
using Newtonsoft.Json;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace agent.Jobs
{
    public abstract class MetricJob : IJob
    {
        protected string metricAddress = "";
        protected IHttpClientFactory httpClient;
        protected MetricsRepository repositoryMetrics;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Task Execute(IJobExecutionContext context)
        {
            AgentRepository repositoryAgents = new AgentRepository();
            List<AgentInfo> agents = repositoryAgents.GetAgents();
            for (int i = 0; i < agents.Count; i++)
            {
                TimeSpan fromTime = TimeSpan.FromSeconds(repositoryMetrics.GetLastTime());
                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                string agentAddress = agents[i].AgentAddress;
                HttpClient client = httpClient.CreateClient();
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{agentAddress}/{metricAddress}/from/{fromTime}/to/{toTime}");
                HttpResponseMessage response;
                try
                {
                    response = client.SendAsync(httpRequest).Result;
                }
                catch (Exception ex)
                {
                    logger.Error("Подключение не установлено");
                    return Task.FromException(ex);
                }
                
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    List<MetricReaponse> metricsResponse = JsonConvert.DeserializeObject<List<MetricReaponse>>(responseString);
                    for (int j = 0; j < metricsResponse.Count; j++)
                    {
                        repositoryMetrics.Add(agents[i].AgentId, metricsResponse[j].Value, metricsResponse[j].Time);
                    }
                    client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{agentAddress}/{metricAddress}/delete/from/{fromTime}/to/{toTime}"));
                }
            }
            return Task.CompletedTask;
        }
    }
}
