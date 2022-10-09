using ValorantAPIApp.Dto.Mission;

namespace ValorantAPIApp.Services.MissionServices
{
    public interface IMissionService
    {
        Task<ResponseAPI<List<MissionGetDto>>> CreateMission(MissionCreateDto missionCreateDto);
        Task<ResponseAPI<List<MissionGetDto>>> GetAllMissions();
        Task<ResponseAPI<string>> GetAllMissionsDetails();
        Task<ResponseAPI<MissionGetDto>> GetMissionByUuid(string uuid);
        Task<ResponseAPI<string>> GetMissionDetailsByUuid(string uuid);
    }
}
