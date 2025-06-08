using AuthService.DTOs;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task RegisterUser(RegisterDto dto);
        Task RegisterCompanyRepresentative(RegisterDto dto);
        Task RegisterArbitrator(RegisterDto dto);
        Task<AuthResponseDto> Login(LoginDto dto);
    }
}
