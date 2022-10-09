using ValorantAPIApp.Dto.AgentLoadout;

namespace ValorantAPIApp.Services.AgentLoadoutServices
{
    public interface IAgentLoadoutService
    {
        Task<ResponseAPI<List<AgentLoadoutGetDto>>> BuyNewAgent(AgentLoadoutBuyDto agentLoadoutCreateDto);
        Task<ResponseAPI<List<AgentLoadoutGetDto>>> GetAllAgentsLoadout();
        Task<ResponseAPI<List<string>>> GetAllAgentsLoadoutDetails();
        Task<ResponseAPI<AgentLoadoutGetDto>> EditAgentLoadout(AgentLoadoutEditDto agentLoadoutEditDto);
        Task<ResponseAPI<AgentLoadoutGetDto>> GetAgentLoadoutByUuid(string uuid);
    }
}
