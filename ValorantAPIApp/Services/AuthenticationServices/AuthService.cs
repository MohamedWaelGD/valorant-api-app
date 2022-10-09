using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.AgentLoadout;
using ValorantAPIApp.Dto.Player;
using ValorantAPIApp.Services.PlayerWeaponsServices;

namespace ValorantAPIApp.Services.AuthenticationServices
{
    public class AuthService : IAuthService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IPlayerWeaponsService _playerWeaponsService;

        public AuthService(ValorantDbContext dbContext, IConfiguration configuration, IMapper mapper, IPlayerWeaponsService playerWeaponsService)
        {
            this._dbContext = dbContext;
            this._configuration = configuration;
            this._mapper = mapper;
            this._playerWeaponsService = playerWeaponsService;
        }

        public async Task<ResponseAPI<string>> Login(PlayerLoginDto playerLoginDto)
        {
            var response = new ResponseAPI<string>();
            var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.Username == playerLoginDto.Username);

            if (player == null)
            {
                response.Success = false;
                response.Message = "Invalid Player";
            }
            else if (!VerifyPasswordHash(playerLoginDto.Password, player.PasswordHash, player.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Invalid Password";
            }
            else
            {
                response.Data = CreateToken(player);
            }

            return response;
        }

        public async Task<ResponseAPI<int>> Register(PlayerRegisterDto playerRegisterDto)
        {
            var response = new ResponseAPI<int>();
            if (await UserExists(playerRegisterDto.Username))
            {
                response.Success = false;
                response.Message = "Username is already exists";
                return response;
            }

            var player = _mapper.Map<Player>(playerRegisterDto);
            CreatePasswordHash(playerRegisterDto.Password, out var passwordHash, out var passwordSalt);
            player.PasswordHash = passwordHash;
            player.PasswordSalt = passwordSalt;

            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();

            var playerWeapon = _mapper.Map<PlayerWeapon>(new Dto.PlayerWeapon.PlayerWeaponBuyDto
            {
                WeaponUuid = "29a0cfab-485b-f5d5-779a-b59f85e204a8"
            });
            playerWeapon.PlayerId = player.Id;

            var loadout = _mapper.Map<AgentLoadout>(new AgentLoadoutBuyDto
            {
                AgentUuid = "f94c3b30-42be-e959-889c-5aa313dba261"
            });
            loadout.PlayerId = player.Id;
            loadout.WeaponUuid = playerWeapon.WeaponUuid;

            await _dbContext.AgentLoadouts.AddAsync(loadout);
            await _dbContext.PlayerWeapons.AddAsync(playerWeapon);
            await _dbContext.SaveChangesAsync();

            response.Data = player.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _dbContext.Players.AnyAsync(p => p.Username == username);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Player player)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()),
                new Claim(ClaimTypes.Name, player.Username),
                new Claim(ClaimTypes.Role, "Player")
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3599),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static int GetPlayerId(IHttpContextAccessor httpContextAccessor)
            => int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
