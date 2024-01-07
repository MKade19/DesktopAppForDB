using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class UserService : IUserService
    {
        private IUserRepository UserRepository { get; }
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public Task CreateOneAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await UserRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await UserRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateByIdAsync(User user)
        {
            await UserRepository.UpdateByIdAsync(user);
        }
    }
}
