using manager.Repository;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;

namespace manager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly CpuMetricsRepository repository = new CpuMetricsRepository();

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public List<MetricResponse> GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            List<MetricResponse> query = repository.GetByTimePeriod(agentId, fromTime, toTime);
            logger.Trace($"GetCpuMetrics agentId = {agentId} fromTime = {fromTime} toTime = {toTime}");
            return query;
        }
    }
}
