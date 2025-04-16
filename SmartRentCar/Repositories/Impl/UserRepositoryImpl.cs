using SmartRentCar.Config;
using SmartRentCar.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with Id {userId} not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> SaveUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user.UserId;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteUserById(int userId)
        {
            try
            {
                var user = await GetUserById(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
