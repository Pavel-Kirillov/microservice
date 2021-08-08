using agent.Class;
using agent.Repository;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace agent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class AgentNetworkMetricsController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly NetworkMetricsRepository repository;
        public AgentNetworkMetricsController(NetworkMetricsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public List<MetricReaponse> GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            List<MetricReaponse> query = repository.GetByTimePeriod(fromTime, toTime);
            logger.Trace($"GetAgentMetrics fromTime = {fromTime} toTime = {toTime}");
            return query;
        }
        [HttpDelete("delete/from/{fromTime}/to/{toTime}")]
        public IActionResult Delete([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            repository.Delete(fromTime, toTime);
            logger.Trace($"Delete fromTime = {fromTime} toTime = {toTime}");
            return Ok();
        }
    }
}
