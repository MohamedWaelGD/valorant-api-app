using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.Agent;
using ValorantAPIApp.Models;

namespace ValorantAPIApp.Services.AgentServices
{
    public class AgentService : IAgentService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpService _httpService;

        public AgentService(ValorantDbContext dbContext, IMapper mapper, HttpService httpService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpService = httpService;
        }

        public async Task<ResponseAPI<List<AgentGetDto>>> CreateAgent(AgentCreateDto agentCreateDto)
        {
            var response = new ResponseAPI<List<AgentGetDto>>();
            var agent = _mapper.Map<Agent>(agentCreateDto);
            await _dbContext.Agents.AddAsync(agent);
            await _dbContext.SaveChangesAsync();

            response.Data = _dbContext.Agents.Select(e => _mapper.Map<AgentGetDto>(e)).ToList();

            return response;
        }

        public async Task<ResponseAPI<AgentGetDto>> GetAgentByUuid(string uuid)
        {
            var response = new ResponseAPI<AgentGetDto>();

            response.Data = await _dbContext.Agents.Where(e => e.Uuid == uuid).Select(e => _mapper.Map<AgentGetDto>(e)).FirstOrDefaultAsync();

            return response;
        }

        public async Task<ResponseAPI<string>> GetAgentDetailsByUuid(string uuid)
        {
            return await _httpService.Request("agents/" + uuid, HttpMethod.Get);
        }

        public async Task<ResponseAPI<List<AgentGetDto>>> GetAllAgents()
        {
            var response = new ResponseAPI<List<AgentGetDto>>();

            response.Data = await _dbContext.Agents.Select(e => _mapper.Map<AgentGetDto>(e)).ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<string>> GetAllAgentsDetails()
        {
            return await _httpService.Request("agents?isPlayableCharacter=true", HttpMethod.Get);
        }
    }
}
