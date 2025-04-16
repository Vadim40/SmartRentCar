using AutoMapper;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Repositories;

namespace SmartRentCar.Services.Impl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServiceImpl(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task DeleteUserById(int userId)
        {
            await _userRepository.DeleteUserById(userId);
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<int> SaveUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            return await _userRepository.SaveUser(user);
        }

        public async Task UpdateUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _userRepository.UpdateUser(user);
        }
    }
}
