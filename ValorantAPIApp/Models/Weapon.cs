using System.ComponentModel.DataAnnotations;

namespace ValorantAPIApp.Models
{
    public class Weapon
    {
        [Key]
        public string Uuid { get; set; }
        public int Price { get; set; } = 0;
        public List<PlayerWeapon>? PlayerWeapons { get; set; }
        public List<AgentLoadout>? AgentLoadouts { get; set; }
        public List<WeaponSkin>? WeaponSkins { get; set; }
    }
}
