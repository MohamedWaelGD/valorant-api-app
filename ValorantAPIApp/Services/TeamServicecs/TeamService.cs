using AutoMapper;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.Team;
using ValorantAPIApp.Services.AuthenticationServices;

namespace ValorantAPIApp.Services.TeamServicecs
{
    public class TeamService : ITeamService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TeamService(ValorantDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseAPI<TeamGetDto>> AddAgentToTeam(TeamUpdateAgentDto teamUpdateAgentDto)
        {
            var response = new ResponseAPI<TeamGetDto>();

            var player = await _dbContext.Players
                .Include(e => e.EquippedAgentsLoadouts)
                .FirstOrDefaultAsync(e => e.Id == AuthService.GetPlayerId(_httpContextAccessor));

            var team = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .FirstOrDefaultAsync(e => e.Id == teamUpdateAgentDto.teamId);
            
            if (team == null)
            {
                response.Success = false;
                response.Message = "Team is not exists";
                return response;
            }

            if (team.Agents.Count >= team.maxTeamSize)
            {
                response.Success = false;
                response.Message = "Team has reached to its limit";
                return response;
            }

            var agent = await _dbContext.Agents.FindAsync(teamUpdateAgentDto.agentUuid);


            if (agent == null)
            {
                response.Success = false;
                response.Message = "Agent is not exists";
                return response;
            }

            if (!await IsPlayerHasAgent(agent, player))
            {
                response.Success = false;
                response.Message = "Player does not have this agent";
                return response;
            }

            if (await IsAgentAlreadyInteam(agent))
            {
                response.Success = false;
                response.Message = "Agent is already in team";
            }

            if (team.Agents.Contains(agent))
            {
                response.Success = false;
                response.Message = "Agent is already in team";
                return response;
            }

            team.Agents.Add(agent);
            await _dbContext.SaveChangesAsync();

            response.Data = _mapper.Map<TeamGetDto>(team);

            return response;
        }

        public async Task<ResponseAPI<List<TeamGetDto>>> CreateTeam(TeamCreateDto teamCreateDto)
        {
            var response = new ResponseAPI<List<TeamGetDto>>();

            var player = await _dbContext.Players.Include(e => e.TeamsOwned).FirstOrDefaultAsync(e => e.Id == AuthService.GetPlayerId(_httpContextAccessor));

            if (player.TeamsOwned.Count >= player.MaxTeams)
            {
                response.Success = false;
                response.Message = "You have reached the max of your teams";
            }
            else
            {
                var team = _mapper.Map<Team>(teamCreateDto);
                team.Player = player;

                await _dbContext.Teams.AddAsync(team);
                await _dbContext.SaveChangesAsync();
            }

            response.Data = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<TeamGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<List<TeamGetDto>>> DeleteTeam(int id)
        {
            var response = new ResponseAPI<List<TeamGetDto>>();

            var team = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .FirstOrDefaultAsync(e => e.Id == id);

            if (team == null)
            {
                response.Success = false;
                response.Message = "Team not found";
            }
            else
            {
                _dbContext.Teams.Remove(team);
                await _dbContext.SaveChangesAsync();
            }

            response.Data = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<TeamGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<List<TeamGetDto>>> GetAllTeam()
        {
            var response = new ResponseAPI<List<TeamGetDto>>();

            response.Data = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .Select(e => _mapper.Map<TeamGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<TeamGetDto>> GetTeamById(int id)
        {
            var response = new ResponseAPI<TeamGetDto>();

            response.Data = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor) &&
                    e.Id == id)
                .Select(e => _mapper.Map<TeamGetDto>(e))
                .FirstOrDefaultAsync();

            return response;
        }

        public async Task<ResponseAPI<TeamGetDto>> RemoveAgentFromTeam(TeamUpdateAgentDto teamUpdateAgentDto)
        {
            var response = new ResponseAPI<TeamGetDto>();

            var team = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .FirstOrDefaultAsync(e => e.Id == teamUpdateAgentDto.teamId);

            if (team == null)
            {
                response.Success = false;
                response.Message = "Team is not exists";
                return response;
            }

            var agent = await _dbContext.Agents.FindAsync(teamUpdateAgentDto.agentUuid);

            if (agent == null)
            {
                response.Success = false;
                response.Message = "Agent is not exists";
                return response;
            }

            team.Agents.Remove(agent);
            await _dbContext.SaveChangesAsync();

            response.Data = _mapper.Map<TeamGetDto>(team);

            return response;
        }

        public async Task<ResponseAPI<TeamGetDto>> AssignTeamToMission(TeamAssignDto teamAssignDto)
        {
            var response = new ResponseAPI<TeamGetDto>();

            var team = await _dbContext.Teams
                .Include(e => e.Player)
                .Include(e => e.Agents)
                .Include(e => e.Mission)
                .Where(e => e.Player.Id == AuthService.GetPlayerId(_httpContextAccessor))
                .FirstOrDefaultAsync(e => e.Id == teamAssignDto.teamId);

            if (team == null)
            {
                response.Success = false;
                response.Message = "Team is not exists";
                return response;
            }

            if (team.Mission != null)
            {
                response.Success = false;
                response.Message = "Team is already in mission";
                return response;
            }

            var mission = await _dbContext.Missions.FindAsync(teamAssignDto.MissionUuid);

            if (mission == null)
            {
                response.Success = false;
                response.Message = "Mission is not exists";
                return response;
            }

            team.Mission = mission;
            team.MissionEndTime = DateTime.Now.AddMinutes(mission.DurationInMin);

            response.Data = _mapper.Map<TeamGetDto>(team);

            await _dbContext.SaveChangesAsync();

            return response;
        }

        private async Task<bool> IsPlayerHasAgent(Agent agent, Player player)
        {
            return await _dbContext.AgentLoadouts.Include(e => e.Agent).Include(e => e.Player).AnyAsync(e => e.Agent == agent && e.Player == player);
        }

        private async Task<bool> IsAgentAlreadyInteam(Agent agent)
        {
            return await _dbContext.Teams.Include(e => e.Agents).AnyAsync(e => e.Agents.Contains(agent));
        }
    }
}
