using agent.Repository;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace agent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class AgentHddMetricsController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly HddMetricsRepository repository;
        public AgentHddMetricsController(HddMetricsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public List<MetricResponse> GetMetricsFromAgent([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            List<MetricResponse> query = repository.GetByTimePeriod(fromTime, toTime);
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
