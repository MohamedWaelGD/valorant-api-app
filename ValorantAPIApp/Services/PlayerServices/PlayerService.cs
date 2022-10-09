using AutoMapper;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.Player;
using ValorantAPIApp.Services.AuthenticationServices;

namespace ValorantAPIApp.Services.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlayerService(ValorantDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseAPI<List<PlayerGetDto>>> GetOtherPlayersDetails()
        {
            var response = new ResponseAPI<List<PlayerGetDto>>();

            response.Data = await _dbContext.Players
                .Where(e => e.Id != AuthService.GetPlayerId(_httpContextAccessor))
                .Include(e => e.EquippedAgentsLoadouts)
                .Include(e => e.EquippedWeapons)
                .Include(e => e.EquippedWeaponSkins)
                .Include(e => e.TeamsOwned)
                .Select(e => _mapper.Map<PlayerGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<PlayerGetDto>> GetPlayerDetails()
        {
            var response = new ResponseAPI<PlayerGetDto>();

            response.Data = await _dbContext.Players
                .Include(e => e.EquippedAgentsLoadouts)
                .Include(e => e.EquippedWeapons)
                .Include(e => e.EquippedWeaponSkins)
                .Include(e => e.TeamsOwned)
                .Where(e => e.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<PlayerGetDto>(e))
                .FirstOrDefaultAsync();

            return response;
        }
    }
}
