using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.Agent;
using ValorantAPIApp.Services.AgentServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            this._agentService = agentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseAPI<List<AgentGetDto>>>> CreateAgent(AgentCreateDto agentCreateDto)
        {
            return Ok(await _agentService.CreateAgent(agentCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<List<AgentGetDto>>>> GetAllAgents()
        {
            return Ok(await _agentService.GetAllAgents());
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseAPI<string>>> GetAllAgentsDetails()
        {
            return Ok(await _agentService.GetAllAgentsDetails());
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ResponseAPI<AgentGetDto>>> GetAgentByUuid(string uuid)
        {
            return Ok(await _agentService.GetAgentByUuid(uuid));
        }

        [HttpGet("details/{uuid}")]
        public async Task<ActionResult<ResponseAPI<string>>> GetAgentByUuidWithDetails(string uuid)
        {
            var data = await _agentService.GetAgentDetailsByUuid(uuid);
            return Ok(data);
        }
    }
}
