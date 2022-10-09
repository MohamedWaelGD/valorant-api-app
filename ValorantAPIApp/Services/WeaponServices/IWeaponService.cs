using ValorantAPIApp.Dto.Weapon;
using ValorantAPIApp.Dto.WeaponSkin;
using ValorantAPIApp.Models;

namespace ValorantAPIApp.Services.WeaponServices
{
    public interface IWeaponService
    {
        Task<ResponseAPI<List<WeaponGetDto>>> CreateWeapon(WeaponCreateDto weaponCreateDto);
        Task<ResponseAPI<List<WeaponGetDto>>> GetAllWeapons();
        Task<ResponseAPI<string>> GetAllWeaponsDetails();
        Task<ResponseAPI<WeaponGetDto>> GetWeaponById(string uuid);
        Task<ResponseAPI<string>> GetWeaponDetailsById(string uuid);
    }
}
