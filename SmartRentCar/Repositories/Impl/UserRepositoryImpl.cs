using SmartRentCar.Config;
using SmartRentCar.Models;

namespace SmartRentCar.Repositories.Impl
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<User> GetUserById(int userId)
        {

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"Car with Id {userId} not found");
            }
            return user;

        }

 
        public async Task<int> SaveUser(User user)
        {
            await _context.Users.AddAsync(user); 
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);       
            await _context.SaveChangesAsync();

        }

       
        public async Task DeleteUserById(int userId)
        {
            var user = await GetUserById(userId);    
            if (user != null)
            {
                _context.Users.Remove(user);    
                await _context.SaveChangesAsync(); 
            }
        
        }
    }
}
