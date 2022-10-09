using ValorantAPIApp.Dto.Team;

namespace ValorantAPIApp.Dto.Mission
{
    public class MissionGetDto
    {
        public string Uuid { get; set; }
        public double DurationInMin { get; set; } = 1;
        public double Reward { get; set; } = 500;
        public List<TeamGetDto>? TeamsAssignedIn { get; set; }
    }
}
