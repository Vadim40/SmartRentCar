using AuthService.Config;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories.Impl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUserId(int userId)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task SaveUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
