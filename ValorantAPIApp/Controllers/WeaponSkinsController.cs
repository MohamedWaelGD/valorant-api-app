using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.WeaponSkin;
using ValorantAPIApp.Services.WeaponSkinServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeaponSkinsController : ControllerBase
    {
        private readonly IWeaponSkinService _weaponSkinService;

        public WeaponSkinsController(IWeaponSkinService weaponSkinService)
        {
            this._weaponSkinService = weaponSkinService;
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ResponseAPI<List<WeaponSkinGetDto>>>> GetWeaponSkins(string uuid)
        {
            return Ok(await _weaponSkinService.GetWeaponSkins(uuid));
        }

        [HttpGet("skin/{uuid}")]
        public async Task<ActionResult<ResponseAPI<WeaponSkinGetDto>>> GetWeaponSkinByUuid(string uuid)
        {
            return Ok(await _weaponSkinService.GetWeaponSkinByUuid(uuid));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseAPI<List<WeaponSkinGetDto>>>> CreateWeaponSkin(WeaponSkingCreateDto weaponSkingCreateDto)
        {
            return Ok(await _weaponSkinService.CreateWeaponSkin(weaponSkingCreateDto));
        }

        [HttpGet()]
        public async Task<ActionResult<ResponseAPI<List<WeaponSkinGetDto>>>> GetAllWeaponSkins()
        {
            return Ok(await _weaponSkinService.GetAllWeaponSkins());
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseAPI<string>>> GetAllWeaponSkinsDetails()
        {
            return Ok(await _weaponSkinService.GetAllWeaponSkinsDetails());
        }

        [HttpGet("Equipped/{uuid}")]
        public async Task<ActionResult<ResponseAPI<List<WeaponSkinGetDto>>>> GetWeaponEquippedSkins(string uuid)
        {
            var response = await _weaponSkinService.GetWeaponEquippedSkins(uuid);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("details/{uuid}")]
        public async Task<ActionResult<ResponseAPI<string>>> GetWeaponSkinsDetails(string uuid)
        {
            var response = await _weaponSkinService.GetWeaponSkinsDetails(uuid);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("Equipped/details/{uuid}")]
        public async Task<ActionResult<ResponseAPI<string>>> GetWeaponEquippedSkinsDetails(string uuid)
        {
            var response = await _weaponSkinService.GetWeaponEquippedSkinsDetails(uuid);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
