using agent.Class;
using manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace appMetrics.Class
{

    public class Metrics
    {
        protected string metricAddress = "";
        private readonly string metricManagerAddress = "http://localhost:7993";
        private List<T> Request<T>(string request)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, request);
            HttpResponseMessage response;
            response = httpClient.SendAsync(httpRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<T>>(responseString);
            }
            return null;
        }
        private void RequestPost(string request)
        {
            _ = new HttpClient().SendAsync(new HttpRequestMessage(HttpMethod.Post, request)).Result;
        }
        private void RequestDelete(string request)
        {
            _ = new HttpClient().SendAsync(new HttpRequestMessage(HttpMethod.Delete, request)).Result;
        }
        public List<MetricReaponse> GetMetricsAgent(TimeSpan fromTime, TimeSpan toTime)
        {

            return Request<MetricReaponse>($"{MainWindow.agentAddress}/{metricAddress}/{MainWindow.agentID}/from/{fromTime}/to/{toTime}");
        }
        public List<AgentInfo> GetAgents()
        {
            return Request<AgentInfo>($"{metricManagerAddress}/api/manager/list");
        }
        public void AddAgent(int agentID,string agentAddress)
        {
            RequestPost($"{metricManagerAddress}/api/manager/register?agentId={agentID}&agentAddress={agentAddress}");
        }
        public void DeleteAgent(int agentID)
        {
            RequestDelete($"{metricManagerAddress}/api/manager/delete?agentId={agentID}");
        }
    }
}
