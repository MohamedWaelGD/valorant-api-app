using ValorantAPIApp.Dto.Player;

namespace ValorantAPIApp.Services.PlayerServices
{
    public interface IPlayerService
    {
        Task<ResponseAPI<PlayerGetDto>> GetPlayerDetails();
        Task<ResponseAPI<List<PlayerGetDto>>> GetOtherPlayersDetails();
    }
}
