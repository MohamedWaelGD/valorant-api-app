using AutoMapper;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.PlayerWeapon;
using ValorantAPIApp.Services.AuthenticationServices;

namespace ValorantAPIApp.Services.PlayerWeaponsServices
{
    public class PlayerWeaponsService : IPlayerWeaponsService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlayerWeaponsService(ValorantDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseAPI<List<PlayerWeaponGetDto>>> BuyWeapon(PlayerWeaponBuyDto playerWeaponBuyDto)
        {
            var response = new ResponseAPI<List<PlayerWeaponGetDto>>();

            var weapon = await _dbContext.Weapons.FindAsync(playerWeaponBuyDto.WeaponUuid);

            if (weapon == null)
            {
                response.Success = false;
                response.Message = "Weapon not found";
            }
            else
            {
                var player = await _dbContext.Players.FindAsync(AuthService.GetPlayerId(_httpContextAccessor));

                if (weapon.Price > player.Currency)
                {
                    response.Success = false;
                    response.Message = "Player does not have enough money";
                }
                else
                {
                    var playerWeapon = _mapper.Map<PlayerWeapon>(playerWeaponBuyDto);
                    playerWeapon.PlayerId = AuthService.GetPlayerId(_httpContextAccessor);

                    player.Currency -= weapon.Price;
                    await _dbContext.PlayerWeapons.AddAsync(playerWeapon);
                    await _dbContext.SaveChangesAsync();
                }
            }

            response.Data = await _dbContext.PlayerWeapons
                .Include(e => e.Player)
                .Include(e => e.Weapon)
                .Include(e => e.WeaponSkin)
                .Where(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<PlayerWeaponGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<PlayerWeaponGetDto>> EditPlayerWeapon(PlayerWeaponEditDto agentLoadoutEditDto)
        {
            var response = new ResponseAPI<PlayerWeaponGetDto>();

            var playerWeapon = await _dbContext.PlayerWeapons
                .Include(e => e.WeaponSkin)
                .Include(e => e.Weapon)
                .Where(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor) &&
                    e.WeaponUuid == agentLoadoutEditDto.WeaponUuid)
                .FirstOrDefaultAsync();

            if (playerWeapon == null)
            {
                response.Success = false;
                response.Message = "Player does not own this weapon";
                return response;
            }

            var player = await _dbContext.Players.FindAsync(AuthService.GetPlayerId(_httpContextAccessor));
            var skin = await _dbContext.WeaponSkins
                .Include(e => e.Players)
                .FirstOrDefaultAsync(e => e.Players.Contains(player));

            if (skin == null)
            {
                response.Success = false;
                response.Message = "Player does not own this skin";
                return response;
            }

            playerWeapon.WeaponSkin = skin;
            await _dbContext.SaveChangesAsync();

            response.Data = _mapper.Map<PlayerWeaponGetDto>(playerWeapon);

            return response;
        }

        public async Task<ResponseAPI<List<PlayerWeaponGetDto>>> GetAllPlayerWeapons()
        {
            var response = new ResponseAPI<List<PlayerWeaponGetDto>>();

            response.Data = await _dbContext.PlayerWeapons
                .Include(e => e.Player)
                .Include(e => e.Weapon)
                .Include(e => e.WeaponSkin)
                .Where(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<PlayerWeaponGetDto>(e))
                .ToListAsync();

            return response;
        }
    }
}
