using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValorantAPIApp.Models
{
    public class PlayerWeapon
    {
        [ForeignKey(nameof(Weapon))]
        public string WeaponUuid { get; set; }
        public Weapon Weapon { get; set; }
        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public WeaponSkin? WeaponSkin { get; set; }
    }
}
