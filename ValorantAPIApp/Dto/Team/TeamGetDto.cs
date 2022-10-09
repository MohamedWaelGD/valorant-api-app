using ValorantAPIApp.Dto.Agent;
using ValorantAPIApp.Dto.Mission;

namespace ValorantAPIApp.Dto.Team
{
    public class TeamGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MissionGetDto? Mission { get; set; }
        public List<AgentGetDto>? Agents { get; set; }
        public int maxTeamSize { get; set; }
    }
}
