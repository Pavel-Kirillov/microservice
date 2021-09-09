using agent.Repository;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace agent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class AgentRamMetricsController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly RamMetricsRepository repository;
        public AgentRamMetricsController(RamMetricsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
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
