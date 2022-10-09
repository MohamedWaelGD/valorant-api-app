using ValorantAPIApp.Dto.AgentLoadout;
using ValorantAPIApp.Dto.PlayerWeapon;
using ValorantAPIApp.Dto.Team;
using ValorantAPIApp.Dto.WeaponSkin;

namespace ValorantAPIApp.Dto.Player
{
    public class PlayerGetDto
    {
        public string? DisplayName { get; set; }
        public int Currency { get; set; } = 1000;
        public int MaxTeams { get; set; } = 1;
        public List<AgentLoadoutGetDto>? EquippedAgentsLoadouts { get; set; }
        public List<TeamGetDto>? TeamsOwned { get; set; }
        public List<PlayerWeaponGetDto>? EquippedWeapons { get; set; }
        public List<WeaponSkinGetDto>? EquippedWeaponSkins { get; set; }
    }
}
