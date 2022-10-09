using AutoMapper;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.Weapon;
using ValorantAPIApp.Dto.WeaponSkin;
using ValorantAPIApp.Services.AuthenticationServices;

namespace ValorantAPIApp.Services.WeaponServices
{
    public class WeaponService : IWeaponService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly HttpService _httpService;
        private IHttpContextAccessor _httpContextAccessor;

        public WeaponService(ValorantDbContext dbContext, IMapper mapper, HttpService httpService, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpService = httpService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseAPI<List<WeaponGetDto>>> CreateWeapon(WeaponCreateDto weaponCreateDto)
        {
            var response = new ResponseAPI<List<WeaponGetDto>>();
            var weapon = _mapper.Map<Weapon>(weaponCreateDto);
            await _dbContext.Weapons.AddAsync(weapon);
            await _dbContext.SaveChangesAsync();

            response.Data = _dbContext.Weapons.Select(e => _mapper.Map<WeaponGetDto>(e)).ToList();

            return response;
        }

        public async Task<ResponseAPI<List<WeaponGetDto>>> GetAllWeapons()
        {
            var response = new ResponseAPI<List<WeaponGetDto>>();

            response.Data = await _dbContext.Weapons.Select(e => _mapper.Map<WeaponGetDto>(e)).ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<WeaponGetDto>> GetWeaponById(string uuid)
        {
            var response = new ResponseAPI<WeaponGetDto>();

            response.Data = await _dbContext.Weapons.Where(e => e.Uuid == uuid).Select(e => _mapper.Map<WeaponGetDto>(e)).FirstOrDefaultAsync();

            return response;
        }

        public async Task<ResponseAPI<string>> GetWeaponDetailsById(string uuid)
        {
            var response = new ResponseAPI<string>();

            return await _httpService.Request("weapons/" + uuid, HttpMethod.Get);
        }

        public async Task<ResponseAPI<string>> GetAllWeaponsDetails()
        {
            return await _httpService.Request("weapons", HttpMethod.Get);
        }
    }
}
