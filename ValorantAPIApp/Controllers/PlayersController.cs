using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.Player;
using ValorantAPIApp.Services.PlayerServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            this._playerService = playerService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<ResponseAPI<PlayerGetDto>>> GetPlayerDetails()
        {
            return Ok(await _playerService.GetPlayerDetails());
        }

        [HttpGet("otherProfiles")]
        public async Task<ActionResult<ResponseAPI<List<PlayerGetDto>>>> GetOtherPlayersDetails()
        {
            return Ok(await _playerService.GetOtherPlayersDetails());
        }
    }
}
