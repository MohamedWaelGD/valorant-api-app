using System.ComponentModel.DataAnnotations;

namespace ValorantAPIApp.Models
{
    public class Agent
    {
        [Key]
        public string Uuid { get; set; }
        public int Price { get; set; } = 0;
        public List<AgentLoadout>? Loadouts { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
