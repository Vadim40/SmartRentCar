using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUserId(int userId);
        Task SaveUser(User user);
        Task<bool> EmailExists (string email);
    }
}
