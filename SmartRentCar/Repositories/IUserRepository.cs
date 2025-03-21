using SmartRentCar.Models;

namespace SmartRentCar.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(int userId);

        public Task<int> SaveUser(User user);
        public Task UpdateUser(User user);
        public Task DeleteUserById(int userId);

    }
}
