using ValorantAPIApp.Dto.WeaponSkin;

namespace ValorantAPIApp.Services.WeaponSkinServices
{
    public interface IWeaponSkinService
    {
        Task<ResponseAPI<List<WeaponSkinGetDto>>> CreateWeaponSkin(WeaponSkingCreateDto weaponSkingCreateDto);
        Task<ResponseAPI<List<WeaponSkinGetDto>>> BuyWeaponSkin(WeaponSkinBuyDto weaponSkinBuyDto);
        Task<ResponseAPI<List<WeaponSkinGetDto>>> GetAllWeaponSkins();
        Task<ResponseAPI<string>> GetAllWeaponSkinsDetails();
        Task<ResponseAPI<List<WeaponSkinGetDto>>> GetWeaponSkins(string uuid);
        Task<ResponseAPI<WeaponSkinGetDto>> GetWeaponSkinByUuid(string uuid);
        Task<ResponseAPI<List<WeaponSkinGetDto>>> GetWeaponEquippedSkins(string uuid);
        Task<ResponseAPI<string>> GetWeaponSkinsDetails(string uuid);
        Task<ResponseAPI<string>> GetWeaponEquippedSkinsDetails(string uuid);
    }
}
