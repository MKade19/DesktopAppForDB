using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class RepairmentDataService : HttpDataServiceBase, IRepairmentDataService
    {
        private const string REPAIRMENT_URL = "repairments";

        public async Task CreateOneAsync(Repairment repairment)
        {
            JsonContent content = JsonContent.Create(repairment);
            await PostAsync(content, REPAIRMENT_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(REPAIRMENT_URL + "/" + id);
        }

        public async Task<IEnumerable<Repairment>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<Repairment>>(await GetAsync(REPAIRMENT_URL)) ?? new List<Repairment>();
        }

        public async Task<Repairment> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<Repairment>(await GetAsync(REPAIRMENT_URL + "/" + id)) ?? new Repairment();
        }

        public async Task UpdateByIdAsync(Repairment repairment)
        {
            JsonContent content = JsonContent.Create(repairment);
            await PutAsync(content, REPAIRMENT_URL);
        }
    }
}
