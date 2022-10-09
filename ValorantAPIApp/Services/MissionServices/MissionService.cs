using AutoMapper;
using System.Security.Claims;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.Mission;

namespace ValorantAPIApp.Services.MissionServices
{
    public class MissionService : IMissionService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpService _httpService;

        public MissionService(ValorantDbContext dbContext, IMapper mapper, HttpService httpService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpService = httpService;
        }

        public async Task<ResponseAPI<List<MissionGetDto>>> CreateMission(MissionCreateDto missionCreateDto)
        {
            var response = new ResponseAPI<List<MissionGetDto>>();
            var mission = _mapper.Map<Mission>(missionCreateDto);
            await _dbContext.Missions.AddAsync(mission);
            await _dbContext.SaveChangesAsync();

            response.Data = _dbContext.Missions
                .Include(e => e.TeamsAssignedIn)
                .Select(e => _mapper.Map<MissionGetDto>(e))
                .ToList();

            return response;
        }

        public async Task<ResponseAPI<List<MissionGetDto>>> GetAllMissions()
        {
            var response = new ResponseAPI<List<MissionGetDto>>();

            response.Data = await _dbContext.Missions.Include(e => e.TeamsAssignedIn).Select(e => _mapper.Map<MissionGetDto>(e)).ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<string>> GetAllMissionsDetails()
        {
            return await _httpService.Request("maps", HttpMethod.Get);
        }

        public async Task<ResponseAPI<MissionGetDto>> GetMissionByUuid(string uuid)
        {
            var response = new ResponseAPI<MissionGetDto>();

            response.Data = await _dbContext.Missions
                .Include(e => e.TeamsAssignedIn)
                .Where(e => e.Uuid == uuid)
                .Select(e => _mapper.Map<MissionGetDto>(e))
                .FirstOrDefaultAsync();

            return response;
        }

        public async Task<ResponseAPI<string>> GetMissionDetailsByUuid(string uuid)
        {
            return await _httpService.Request("maps/" + uuid, HttpMethod.Get);
        }
    }
}
