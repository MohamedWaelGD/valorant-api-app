using ValorantAPIApp.Dto.Agent;
using ValorantAPIApp.Dto.Weapon;

namespace ValorantAPIApp.Dto.AgentLoadout
{
    public class AgentLoadoutGetDto
    {
        public AgentGetDto Agent { get; set; }
        public WeaponGetDto Weapon { get; set; }
    }
}
