using ValorantAPIApp.Dto.Team;

namespace ValorantAPIApp.Services.TeamServicecs
{
    public interface ITeamService
    {
        Task<ResponseAPI<List<TeamGetDto>>> CreateTeam(TeamCreateDto teamCreateDto);
        Task<ResponseAPI<List<TeamGetDto>>> GetAllTeam();
        Task<ResponseAPI<List<TeamGetDto>>> DeleteTeam(int id);
        Task<ResponseAPI<TeamGetDto>> AddAgentToTeam(TeamUpdateAgentDto teamUpdateAgentDto);
        Task<ResponseAPI<TeamGetDto>> RemoveAgentFromTeam(TeamUpdateAgentDto teamUpdateAgentDto);
        Task<ResponseAPI<TeamGetDto>> GetTeamById(int id);
        Task<ResponseAPI<TeamGetDto>> AssignTeamToMission(TeamAssignDto teamAssignDto);
    }
}
