using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.Mission;
using ValorantAPIApp.Services.MissionServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionController(IMissionService missionService)
        {
            this._missionService = missionService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseAPI<List<MissionGetDto>>>> CreateMission(MissionCreateDto missionCreateDto)
        {
            return Ok(await _missionService.CreateMission(missionCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<List<MissionGetDto>>>> GetAllMissions()
        {
            return Ok(await _missionService.GetAllMissions());
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseAPI<string>>> GetAllMissionsDetails()
        {
            return Ok(await _missionService.GetAllMissionsDetails());
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ResponseAPI<MissionGetDto>>> GetMissionByUuid(string uuid)
        {
            return Ok(await _missionService.GetMissionByUuid(uuid));
        }

        [HttpGet("details/{uuid}")]
        public async Task<ActionResult<ResponseAPI<string>>> GetMissionByUuidWithDetails(string uuid)
        {
            var data = await _missionService.GetMissionDetailsByUuid(uuid);
            return Ok(data);
        }
    }
}
