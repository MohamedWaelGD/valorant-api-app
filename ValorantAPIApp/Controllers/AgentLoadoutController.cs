using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.AgentLoadout;
using ValorantAPIApp.Services.AgentLoadoutServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgentLoadoutController : ControllerBase
    {
        private readonly IAgentLoadoutService _agentLoadoutService;

        public AgentLoadoutController(IAgentLoadoutService agentLoadoutService)
        {
            this._agentLoadoutService = agentLoadoutService;
        }

        [HttpPost("buy")]
        public async Task<ActionResult<ResponseAPI<List<AgentLoadoutGetDto>>>> BuyNewAgent(AgentLoadoutBuyDto agentLoadoutCreateDto)
        {
            return Ok(await _agentLoadoutService.BuyNewAgent(agentLoadoutCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<List<AgentLoadoutGetDto>>>> GetAllAgentsLoadout()
        {
            return Ok(await _agentLoadoutService.GetAllAgentsLoadout());
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseAPI<List<string>>>> GetAllAgentsLoadoutDetails()
        {
            return Ok(await _agentLoadoutService.GetAllAgentsLoadoutDetails());
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ResponseAPI<AgentLoadoutGetDto>>> GetAgentLoadoutByUuid(string uuid)
        {
            return Ok(await _agentLoadoutService.GetAgentLoadoutByUuid(uuid));
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI<AgentLoadoutGetDto>>> EditAgentLoadout(AgentLoadoutEditDto agentLoadoutEditDto)
        {
            var response = await _agentLoadoutService.EditAgentLoadout(agentLoadoutEditDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
