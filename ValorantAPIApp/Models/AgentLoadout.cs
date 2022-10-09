using System.ComponentModel.DataAnnotations.Schema;

namespace ValorantAPIApp.Models
{
    public class AgentLoadout
    {
        [ForeignKey(nameof(Agent))]
        public string AgentUuid { get; set; }
        public Agent Agent { get; set; }
        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        [ForeignKey(nameof(Weapon))]
        public string WeaponUuid { get; set; }
        public Weapon Weapon { get; set; }
    }
}
