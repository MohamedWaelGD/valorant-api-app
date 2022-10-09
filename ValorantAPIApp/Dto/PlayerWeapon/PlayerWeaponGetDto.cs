using ValorantAPIApp.Dto.Weapon;
using ValorantAPIApp.Dto.WeaponSkin;

namespace ValorantAPIApp.Dto.PlayerWeapon
{
    public class PlayerWeaponGetDto
    {
        public WeaponGetDto Weapon { get; set; }
        public WeaponSkinGetDto? WeaponSkin { get; set; }
    }
}
