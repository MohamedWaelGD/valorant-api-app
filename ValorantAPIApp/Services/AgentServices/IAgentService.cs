using Newtonsoft.Json.Linq;
using ValorantAPIApp.Dto.Agent;
using ValorantAPIApp.Models;

namespace ValorantAPIApp.Services.AgentServices
{
    public interface IAgentService
    {
        Task<ResponseAPI<List<AgentGetDto>>> CreateAgent(AgentCreateDto agentCreateDto);
        Task<ResponseAPI<List<AgentGetDto>>> GetAllAgents();
        Task<ResponseAPI<string>> GetAllAgentsDetails();
        Task<ResponseAPI<AgentGetDto>> GetAgentByUuid(string uuid);
        Task<ResponseAPI<string>> GetAgentDetailsByUuid(string uuid);
    }
}
