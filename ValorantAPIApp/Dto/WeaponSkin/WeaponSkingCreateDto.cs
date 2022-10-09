namespace ValorantAPIApp.Dto.WeaponSkin
{
    public class WeaponSkingCreateDto
    {
        public string Uuid { get; set; }
        public int Price { get; set; } = 0;
        public string WeaponUuid { get; set; }
    }
}
