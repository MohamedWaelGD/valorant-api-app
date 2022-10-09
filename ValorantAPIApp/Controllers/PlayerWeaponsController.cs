using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.PlayerWeapon;
using ValorantAPIApp.Services.PlayerWeaponsServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayerWeaponsController : ControllerBase
    {
        private readonly IPlayerWeaponsService _playerWeaponsService;

        public PlayerWeaponsController(IPlayerWeaponsService playerWeaponsService)
        {
            this._playerWeaponsService = playerWeaponsService;
        }

        [HttpPost("buy")]
        public async Task<ActionResult<ResponseAPI<List<PlayerWeaponGetDto>>>> BuyWeapon(PlayerWeaponBuyDto agentLoadoutCreateDto)
        {
            var response = await _playerWeaponsService.BuyWeapon(agentLoadoutCreateDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<List<PlayerWeaponGetDto>>>> GetAllPlayerWeapons()
        {
            return await _playerWeaponsService.GetAllPlayerWeapons();
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI<PlayerWeaponGetDto>>> EditPlayerWeapon(PlayerWeaponEditDto agentLoadoutEditDto)
        {
            var response = await _playerWeaponsService.EditPlayerWeapon(agentLoadoutEditDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
