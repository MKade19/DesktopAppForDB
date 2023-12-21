using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class UserDataService : HttpDataServiceBase, IUserDataService
    {
        private const string USER_URL = "users";
        public Task CreateOneAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(USER_URL + "/" + id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<User>>(await GetAsync(USER_URL)) ?? new List<User>();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateByIdAsync(User user)
        {
            JsonContent content = JsonContent.Create(user);
            await PutAsync(content, USER_URL);
        }
    }
}
