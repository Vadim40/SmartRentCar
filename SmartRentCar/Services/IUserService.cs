using SmartRentCar.DTO;
using SmartRentCar.Models;

namespace SmartRentCar.Services
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserById(int userId);

        public Task<int> SaveUser(UserDTO user);

        public Task UpdateUser(UserDTO user);
        public Task DeleteUserById(int userId);
    }
}
