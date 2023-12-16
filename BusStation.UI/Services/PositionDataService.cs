using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class PositionDataService : HttpDataServiceBase, IPositionDataService
    {
        public const string POSITION_URL = "positions";
        public async Task CreateOneAsync(Position position)
        {
            JsonContent content = JsonContent.Create(position);
            await PostAsync(content, POSITION_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(POSITION_URL + "/" + id);
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<Position>>(await GetAsync(POSITION_URL)) ?? new List<Position>();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<Position>(await GetAsync(POSITION_URL + "/" + id)) ?? new Position();
        }

        public async Task UpdateByIdAsync(Position position)
        {
            JsonContent content = JsonContent.Create(position);
            await PutAsync(content, POSITION_URL);
        }
    }
}
