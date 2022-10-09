
using System.ComponentModel.DataAnnotations;

namespace ValorantAPIApp.Models
{
    public class WeaponSkin
    {
        [Key]
        public string Uuid { get; set; }
        public int Price { get; set; } = 0;
        public Weapon Weapon { get; set; }
        public List<Player>? Players { get; set; }
    }
}
