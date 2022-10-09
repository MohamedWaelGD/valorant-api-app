namespace ValorantAPIApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Player Player { get; set; }
        public Mission? Mission { get; set; }
        public DateTime MissionEndTime { get; set; }
        public List<Agent>? Agents { get; set; }
        public int maxTeamSize { get; set; } = 5;
    }
}
