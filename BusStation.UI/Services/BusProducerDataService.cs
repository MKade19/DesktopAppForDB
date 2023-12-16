using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class BusProducerDataService : HttpDataServiceBase, IBusProducerDataService
    {
        public const string BUS_PRODUCER_URL = "bus-producers";

        public async Task CreateOneAsync(BusProducer busProducer)
        {
            JsonContent content = JsonContent.Create(busProducer);
            await PostAsync(content, BUS_PRODUCER_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(BUS_PRODUCER_URL + "/" + id);
        }

        public async Task<IEnumerable<BusProducer>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<BusProducer>>(await GetAsync(BUS_PRODUCER_URL)) ?? new List<BusProducer>();
        }

        public async Task<BusProducer> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<BusProducer>(await GetAsync(BUS_PRODUCER_URL + "/" + id)) ?? new BusProducer();
        }

        public async Task UpdateByIdAsync(BusProducer busProducer)
        {
            JsonContent content = JsonContent.Create(busProducer);
            await PutAsync(content, BUS_PRODUCER_URL);
        }
    }
}
