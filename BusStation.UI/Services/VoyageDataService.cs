using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class VoyageDataService : HttpDataServiceBase, IVoyageDataService
    {
        public const string VOYAGE_URL = "voyages";

        public async Task CreateOneAsync(Voyage voyage)
        {
            JsonContent content = JsonContent.Create(voyage);
            await PostAsync(content, VOYAGE_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(VOYAGE_URL + "/" + id);
        }

        public async Task<IEnumerable<Voyage>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<Voyage>>(await GetAsync(VOYAGE_URL)) ?? new List<Voyage>();
        }

        public async Task<Voyage> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<Voyage>(await GetAsync(VOYAGE_URL + "/" + id)) ?? new Voyage();
        }

        public async Task UpdateByIdAsync(Voyage voyage)
        {
            JsonContent content = JsonContent.Create(voyage);
            await PutAsync(content, VOYAGE_URL);
        }
    }
}
