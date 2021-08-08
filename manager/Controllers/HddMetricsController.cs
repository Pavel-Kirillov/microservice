using agent.Class;
using agent.Repository;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace manager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly HddMetricsRepository repository = new HddMetricsRepository();

        [HttpGet("left/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public List<MetricReaponse> GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            List<MetricReaponse> query = repository.GetByTimePeriod(agentId, fromTime, toTime);
            logger.Trace($"GetHddMetrics agentId = {agentId} fromTime = {fromTime} toTime = {toTime}");
            return query;
        }
    }
}
