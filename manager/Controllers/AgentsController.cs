using manager.Repository;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;

namespace manager.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly AgentRepository repository = new AgentRepository();

        [HttpGet("list")]
        public List<AgentInfo> ListAgent()
        {
            logger.Trace($"ListAgent");
            return repository.GetAgents();
        }
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromQuery] int agentId, string agentAddress)
        {
            logger.Trace($"RegisterAgent agentInfo = {agentId},{agentAddress}");
            repository.AddAgent(agentId, agentAddress);
            return Ok();
        }
        [HttpDelete("delete")]
        public IActionResult DeleteAgent([FromQuery] int agentId)
        {
            logger.Trace($"DeleteAgent agentInfo = {agentId}");
            repository.DeleteAgent(agentId);
            return Ok();
        }
    }
}
