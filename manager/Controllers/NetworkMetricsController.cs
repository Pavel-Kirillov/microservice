using agent.Class;
using agent.Repository;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace manager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly NetworkMetricsRepository repository = new NetworkMetricsRepository();

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public List<MetricReaponse> GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            List<MetricReaponse> query = repository.GetByTimePeriod(agentId, fromTime, toTime);
            logger.Trace($"GetNetworkMetrics agentId = {agentId} fromTime = {fromTime} toTime = {toTime}");
            return query;
        }
    }
}
