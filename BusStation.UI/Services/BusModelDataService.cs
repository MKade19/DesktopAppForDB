using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class BusModelDataService : HttpDataServiceBase, IBusModelDataService
    {
        public const string BUS_MODEL_URL = "bus-models";

        public async Task CreateOneAsync(BusModel busModel)
        {
            JsonContent content = JsonContent.Create(busModel);
            await PostAsync(content, BUS_MODEL_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(BUS_MODEL_URL + "/" + id);
        }

        public async Task<IEnumerable<BusModel>> GetAllAsync()
        {
            string modelsString = await GetAsync(BUS_MODEL_URL);
            return JsonSerializer.Deserialize<List<BusModel>>(modelsString) ?? new List<BusModel>();
        }

        public async Task<BusModel> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<BusModel>(await GetAsync(BUS_MODEL_URL + "/" + id)) ?? new BusModel();
        }

        public async Task UpdateByIdAsync(BusModel busModel)
        {
            JsonContent content = JsonContent.Create(busModel);
            await PutAsync(content, BUS_MODEL_URL);
        }
    }
}
