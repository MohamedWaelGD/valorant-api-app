namespace ValorantAPIApp.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? DisplayName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public int Currency { get; set; } = 1000;
        public int MaxTeams { get; set; } = 1;
        public List<AgentLoadout>? EquippedAgentsLoadouts { get; set; }
        public List<Team>? TeamsOwned { get; set; }
        public List<PlayerWeapon>? EquippedWeapons { get; set; }
        public List<WeaponSkin>? EquippedWeaponSkins { get; set; }
    }
}
