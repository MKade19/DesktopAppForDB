using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IUserRepository 
    {
        public Task<User> GetUserByUsernameAsync(string username);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task UpdateByIdAsync(User user);
        public Task CreateOne(User user);
        public Task DeleteByIdAsync(int id);
    }
}
