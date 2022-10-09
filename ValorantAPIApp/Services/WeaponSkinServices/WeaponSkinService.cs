using AutoMapper;
using ValorantAPIApp.Data;
using ValorantAPIApp.Dto.WeaponSkin;
using ValorantAPIApp.Services.AuthenticationServices;

namespace ValorantAPIApp.Services.WeaponSkinServices
{
    public class WeaponSkinService : IWeaponSkinService
    {
        private readonly ValorantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpService _httpService;

        public WeaponSkinService(ValorantDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, HttpService httpService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            this._httpService = httpService;
        }

        public async Task<ResponseAPI<List<WeaponSkinGetDto>>> BuyWeaponSkin(WeaponSkinBuyDto weaponSkinBuyDto)
        {
            var response = new ResponseAPI<List<WeaponSkinGetDto>>();

            var player = await _dbContext.Players.FindAsync(AuthService.GetPlayerId(_httpContextAccessor));
            var weaponSkin = await _dbContext.WeaponSkins.FindAsync(weaponSkinBuyDto.Uuid);

            if (weaponSkin == null)
            {
                response.Success = false;
                response.Message = "Weapon skin not found";
            }
            else
            {
                if (weaponSkin.Price > player.Currency)
                {
                    response.Success = false;
                    response.Message = "Player does not have enough money";
                }
                else
                {
                    player.EquippedWeaponSkins.Add(weaponSkin);
                    player.Currency -= weaponSkin.Price;
                    await _dbContext.SaveChangesAsync();
                }
            }

            response.Data = await _dbContext.WeaponSkins
                .Include(e => e.Players)
                .Where(e => e.Players.Contains(player))
                .Select(e => _mapper.Map<WeaponSkinGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<List<WeaponSkinGetDto>>> GetAllWeaponSkins()
        {
            var response = new ResponseAPI<List<WeaponSkinGetDto>>();

            response.Data = await _dbContext.WeaponSkins
                .Select(e => _mapper.Map<WeaponSkinGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<string>> GetAllWeaponSkinsDetails()
        {
            return await _httpService.Request("weapons/skins", HttpMethod.Get);
        }

        public async Task<ResponseAPI<WeaponSkinGetDto>> GetWeaponSkinByUuid(string uuid)
        {
            var response = new ResponseAPI<WeaponSkinGetDto>();

            response.Data = _mapper.Map<WeaponSkinGetDto>(await _dbContext.WeaponSkins
                .FirstOrDefaultAsync(e => e.Uuid == uuid));



            return response;
        }

        public async Task<ResponseAPI<List<WeaponSkinGetDto>>> GetWeaponSkins(string uuid)
        {
            var response = new ResponseAPI<List<WeaponSkinGetDto>>();

            response.Data = await _dbContext.Weapons
                .Include(e => e.WeaponSkins)
                .Where(e => e.Uuid == uuid)
                .Select(e => _mapper.Map<WeaponSkinGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<List<WeaponSkinGetDto>>> GetWeaponEquippedSkins(string uuid)
        {
            var response = new ResponseAPI<List<WeaponSkinGetDto>>();
            var player = await _dbContext.Players.FindAsync(AuthService.GetPlayerId(_httpContextAccessor));

            var playerWeapon = await _dbContext.PlayerWeapons
                .Include(e => e.Weapon)
                .Include(e => e.Weapon.WeaponSkins)
                .FirstOrDefaultAsync(e => e.Weapon.Uuid == uuid && e.Player == player);

            if (playerWeapon == null)
            {
                response.Success = false;
                response.Message = "Player does not own this weapon";
                return response;
            }

            response.Data = await _dbContext.WeaponSkins
                .Include(e => e.Weapon)
                .Include(e => e.Players)
                .Where(e => e.Weapon == playerWeapon.Weapon && player.EquippedWeaponSkins.Contains(e))
                .Select(e => _mapper.Map<WeaponSkinGetDto>(e))
                .ToListAsync();

            return response;
        }

        public async Task<ResponseAPI<string>> GetWeaponSkinsDetails(string uuid)
        {
            var response = new ResponseAPI<string>();
            var responseEquippedSkins = await GetWeaponSkins(uuid);

            if (!responseEquippedSkins.Success)
            {
                response.Success = false;
                response.Message = "Error happened";
                return response;
            }

            var content = "";
            for (int i = 0; i < responseEquippedSkins.Data.Count; i++)
            {
                content += await _httpService.RequestData("weapons/skins/" + responseEquippedSkins.Data[i].Uuid, HttpMethod.Get);
            }

            return response;
        }

        public async Task<ResponseAPI<string>> GetWeaponEquippedSkinsDetails(string uuid)
        {
            var response = new ResponseAPI<string>();
            var responseEquippedSkins = await GetWeaponEquippedSkins(uuid);

            if (!responseEquippedSkins.Success)
            {
                response.Success = false;
                response.Message = "Error happened";
                return response;
            }

            var content = "";
            for (int i = 0; i < responseEquippedSkins.Data.Count; i++)
            {
                content += await _httpService.RequestData("weapons/skins/" + responseEquippedSkins.Data[i].Uuid, HttpMethod.Get);
            }

            return response;
        }

        public async Task<ResponseAPI<List<WeaponSkinGetDto>>> CreateWeaponSkin(WeaponSkingCreateDto weaponSkingCreateDto)
        {
            var response = new ResponseAPI<List<WeaponSkinGetDto>>();

            var weaponSkin = _mapper.Map<WeaponSkin>(weaponSkingCreateDto);

            await _dbContext.WeaponSkins.AddAsync(weaponSkin);
            await _dbContext.SaveChangesAsync();

            response.Data = await _dbContext.WeaponSkins
                .Include(e => e.Players)
                .Include(e => e.Weapon)
                .Select(e => _mapper.Map<WeaponSkinGetDto>(e))
                .ToListAsync();

            return response;
        }
    }
}
