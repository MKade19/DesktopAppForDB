using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class BusDataService : HttpDataServiceBase, IBusDataService
    {
        private const string BUS_URL = "buses";
        public async Task CreateOneAsync(Bus bus)
        {
            JsonContent content = JsonContent.Create(bus);
            await PostAsync(content, BUS_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(BUS_URL + "/" + id);
        }

        public async Task<IEnumerable<Bus>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<Bus>>(await GetAsync(BUS_URL)) ?? new List<Bus>();
        }

        public async Task<Bus> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<Bus>(await GetAsync(BUS_URL + "/" + id)) ?? new Bus();
        }

        public async Task<IEnumerable<BusColorWithCount>> GetColorsWithCountAsync()
        {
            return JsonSerializer.Deserialize<List<BusColorWithCount>>(await GetAsync(BUS_URL + "/colors-count")) ?? new List<BusColorWithCount>();
        }

        public async Task UpdateByIdAsync(Bus bus)
        {
            JsonContent content = JsonContent.Create(bus);
            await PutAsync(content, BUS_URL);
        }
    }
}
