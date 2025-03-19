using SmartRentCar.Models;

namespace SmartRentCar.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUser(int userId);

        public Task SaveUser(User user);
        public Task UpdateUser(User user);
        public Task DeleteUser(int userId);

    }
}
