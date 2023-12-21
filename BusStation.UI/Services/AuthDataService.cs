using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class AuthDataService : HttpDataServiceBase, IAuthDataService
    {
        private const string AUTH_URL = "auth";
        public async Task LoginAsync(User user)
        {
            JsonContent content = JsonContent.Create(user);
            AuthData authData = JsonSerializer.Deserialize<AuthData>(await PostAsync(content, AUTH_URL + "/login")) ?? new AuthData();
            Properties.Settings.Default.AccessToken = authData.Token;
            Properties.Settings.Default.Role = authData.Role;
        }
    }
}
