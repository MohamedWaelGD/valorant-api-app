using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.Team;
using ValorantAPIApp.Services.TeamServicecs;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            this._teamService = teamService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI<List<TeamGetDto>>>> CreateTeam(TeamCreateDto teamCreateDto)
        {
            var response = await _teamService.CreateTeam(teamCreateDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<List<TeamGetDto>>>> GetAllTeam()
        {
            return Ok(await _teamService.GetAllTeam());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseAPI<List<TeamGetDto>>>> DeleteTeam(int id)
        {
            var response = await _teamService.DeleteTeam(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI<TeamGetDto>>> GetTeamById(int id)
        {
            var response = await _teamService.GetTeamById(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("addAgent")]
        public async Task<ActionResult<ResponseAPI<TeamGetDto>>> AddAgentToTeam(TeamUpdateAgentDto teamUpdateAgentDto)
        {
            var response = await _teamService.AddAgentToTeam(teamUpdateAgentDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("deleteAgent")]
        public async Task<ActionResult<ResponseAPI<TeamGetDto>>> RemoveAgentFromTeam(TeamUpdateAgentDto teamUpdateAgentDto)
        {
            var response = await _teamService.RemoveAgentFromTeam(teamUpdateAgentDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("assignTeam")]
        public async Task<ActionResult<ResponseAPI<TeamGetDto>>> AssignTeamToMission(TeamAssignDto teamAssignDto)
        {
            var response = await _teamService.AssignTeamToMission(teamAssignDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
