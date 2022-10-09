using System.ComponentModel.DataAnnotations;

namespace ValorantAPIApp.Models
{
    public class Mission
    {
        [Key]
        public string Uuid { get; set; }
        public double DurationInMin { get; set; } = 1;
        public double Reward { get; set; } = 500;
        public List<Team>? TeamsAssignedIn { get; set; }
    }
}
