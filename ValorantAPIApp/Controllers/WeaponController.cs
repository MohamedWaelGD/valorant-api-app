using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.Weapon;
using ValorantAPIApp.Dto.WeaponSkin;
using ValorantAPIApp.Services.WeaponServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            this._weaponService = weaponService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseAPI<List<WeaponGetDto>>>> CreateWeapon(WeaponCreateDto weaponCreateDto)
        {
            return Ok(await _weaponService.CreateWeapon(weaponCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI<List<WeaponGetDto>>>> GetAllWeapons()
        {
            return Ok(await _weaponService.GetAllWeapons());
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseAPI<string>>> GetAllWeaponsDetails()
        {
            return Ok(await _weaponService.GetAllWeaponsDetails());
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ResponseAPI<WeaponGetDto>>> GetWeaponByUuid(string uuid)
        {
            return Ok(await _weaponService.GetWeaponById(uuid));
        }

        [HttpGet("details/{uuid}")]
        public async Task<ActionResult<ResponseAPI<string>>> GetWeaponByUuidWithDetails(string uuid)
        {
            var data = await _weaponService.GetWeaponDetailsById(uuid);
            return Ok(data);
        }
    }
}
