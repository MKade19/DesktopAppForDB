using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IUserRepository 
    {
        public Task<User> GetUserByUsername(string username);
        public Task CreateOne(User user);
    }
}
