using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValorantAPIApp.Dto.Player;
using ValorantAPIApp.Services.AuthenticationServices;

namespace ValorantAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseAPI<int>>> Register(PlayerRegisterDto playerRegisterDto)
        {
            var response = await _authService.Register(playerRegisterDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseAPI<string>>> Login(PlayerLoginDto playerLoginDto)
        {
            var response = await _authService.Login(playerLoginDto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
