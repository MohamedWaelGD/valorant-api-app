using ValorantAPIApp.Dto.PlayerWeapon;

namespace ValorantAPIApp.Services.PlayerWeaponsServices
{
    public interface IPlayerWeaponsService
    {
        Task<ResponseAPI<List<PlayerWeaponGetDto>>> BuyWeapon(PlayerWeaponBuyDto agentLoadoutCreateDto);
        Task<ResponseAPI<List<PlayerWeaponGetDto>>> GetAllPlayerWeapons();
        Task<ResponseAPI<PlayerWeaponGetDto>> EditPlayerWeapon(PlayerWeaponEditDto agentLoadoutEditDto);
    }
}
