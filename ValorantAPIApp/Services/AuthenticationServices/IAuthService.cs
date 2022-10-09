using ValorantAPIApp.Dto.Player;

namespace ValorantAPIApp.Services.AuthenticationServices
{
    public interface IAuthService
    {
        Task<ResponseAPI<int>> Register(PlayerRegisterDto player);
        Task<ResponseAPI<string>> Login(PlayerLoginDto playerLoginDto);
        Task<bool> UserExists(string username);
    }
}
