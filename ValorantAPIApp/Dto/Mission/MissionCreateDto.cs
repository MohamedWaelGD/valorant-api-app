namespace ValorantAPIApp.Dto.Mission
{
    public class MissionCreateDto
    {
        public string Uuid { get; set; }
        public double DurationInMin { get; set; } = 1;
        public double Reward { get; set; } = 500;
    }
}
