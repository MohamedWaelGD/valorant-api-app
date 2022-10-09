using AutoMapper;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.AgentLoadout;
using ValorantAPIApp.Services.AuthenticationServices;

namespace ValorantAPIApp.Services.AgentLoadoutServices
{
    public class AgentLoadoutService : IAgentLoadoutService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpService _httpService;

        public AgentLoadoutService(ValorantDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, HttpService httpService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            this._httpService = httpService;
        }

        public async Task<ResponseAPI<List<AgentLoadoutGetDto>>> BuyNewAgent(AgentLoadoutBuyDto agentLoadoutCreateDto)
        {
            var response = new ResponseAPI<List<AgentLoadoutGetDto>>();

            var agent = await _dbContext.Agents.FindAsync(agentLoadoutCreateDto.AgentUuid);

            if (agent == null)
            {
                response.Success = false;
                response.Message = "Agent not found";
                return response;
            }

            if (!await IsCanBuy(agent.Price))
            {
                response.Success = false;
                response.Message = "Player does not have enough money";
                return response;
            }

            var player = await _dbContext.Players.FindAsync(AuthService.GetPlayerId(_httpContextAccessor));

            player.Currency -= agent.Price;

            var loadout = _mapper.Map<AgentLoadout>(agentLoadoutCreateDto);
            loadout.PlayerId = AuthService.GetPlayerId(_httpContextAccessor);
            loadout.WeaponUuid = "29a0cfab-485b-f5d5-779a-b59f85e204a8";

            await _dbContext.AgentLoadouts.AddAsync(loadout);
            await _dbContext.SaveChangesAsync();

            response.Data = await _dbContext.AgentLoadouts
                .Where(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor))
                .Include(e => e.Weapon)
                .Include(e => e.Agent)
                .Select(e => _mapper.Map<AgentLoadoutGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<AgentLoadoutGetDto>> EditAgentLoadout(AgentLoadoutEditDto agentLoadoutEditDto)
        {
            var response = new ResponseAPI<AgentLoadoutGetDto>();

            if (!await IsPlayerHasLoadout(agentLoadoutEditDto.AgentUuid))
            {
                response.Success = false;
                response.Message = "Player does not own the agent";
                return response;
            }

            if (!await IsHasWeapon(agentLoadoutEditDto.WeaponUuid))
            {
                response.Success = false;
                response.Message = "Player does not own this weapon";
                return response;
            }

            var loadout = await _dbContext.AgentLoadouts
                .Include(e => e.Weapon)
                .Include(e => e.Agent)
                .FirstOrDefaultAsync(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor) && e.AgentUuid == agentLoadoutEditDto.AgentUuid);

            loadout.WeaponUuid = agentLoadoutEditDto.WeaponUuid;

            await _dbContext.SaveChangesAsync();

            response.Data = _mapper.Map<AgentLoadoutGetDto>(loadout);

            return response;
        }

        public async Task<ResponseAPI<AgentLoadoutGetDto>> GetAgentLoadoutByUuid(string uuid)
        {
            var response = new ResponseAPI<AgentLoadoutGetDto>();

            response.Data = await _dbContext.AgentLoadouts
                .Include(e => e.Weapon)
                .Include(e => e.Agent)
                .Where(e => e.AgentUuid == uuid &&
                    e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<AgentLoadoutGetDto>(e)).FirstOrDefaultAsync();

            return response;
        }

        public async Task<ResponseAPI<List<AgentLoadoutGetDto>>> GetAllAgentsLoadout()
        {
            var response = new ResponseAPI<List<AgentLoadoutGetDto>>();

            response.Data = await _dbContext.AgentLoadouts
                .Include(e => e.Weapon)
                .Include(e => e.Agent)
                .Where(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<AgentLoadoutGetDto>(e))
                .ToListAsync();

            return response;
        }

        private async Task<bool> IsPlayerHasLoadout(string agentUuid)
        {
            return await _dbContext.AgentLoadouts
                .AnyAsync(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor) && e.AgentUuid == agentUuid);
        }

        private async Task<bool> IsHasWeapon(string weaponUuid)
        {
            return await _dbContext.PlayerWeapons
                .AnyAsync(e => e.PlayerId == AuthService.GetPlayerId(_httpContextAccessor) && e.WeaponUuid == weaponUuid);
        }

        private async Task<bool> IsCanBuy(int price)
        {
            var player = await _dbContext.Players
                .FindAsync(AuthService.GetPlayerId(_httpContextAccessor));

            return player.Currency >= price;
        }

        public async Task<ResponseAPI<List<string>>> GetAllAgentsLoadoutDetails()
        {
            var agentsOwned = await GetAllAgentsLoadout();

            var response = new ResponseAPI<List<string>>();

            response.Data = new List<string>();

            foreach (var agent in agentsOwned.Data)
            {
                var data = await _httpService.Request("agents/" + agent.Agent.Uuid, HttpMethod.Get);
                
                response.Data.Add(data.Data);
            }

            return response;
        }
    }
}
