using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class WorkerDataService : HttpDataServiceBase, IWorkerDataService
    {
        private const string WORKER_URL = "workers";

        public async Task CreateOneAsync(Worker worker)
        {
            JsonContent content = JsonContent.Create(worker);
            await PostAsync(content, WORKER_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(WORKER_URL + "/" + id);
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<Worker>>(await GetAsync(WORKER_URL)) ?? new List<Worker>();
        }

        public async Task<Worker> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<Worker>(await GetAsync(WORKER_URL + "/" + id)) ?? new Worker();
        }

        public async Task<IEnumerable<Worker>> GetDriversAsync()
        {
            return JsonSerializer.
                Deserialize<List<Worker>>(await GetAsync(WORKER_URL + "/position/Driver"))
                ?? new List<Worker>();
        }

        public async Task<IEnumerable<Worker>> GetMechanicsAsync()
        {
            return JsonSerializer.
                Deserialize<List<Worker>>(await GetAsync(WORKER_URL + "/position/Mechanic"))
                ?? new List<Worker>();
        }

        public async Task UpdateByIdAsync(Worker worker)
        {
            JsonContent content = JsonContent.Create(worker);
            await PutAsync(content, WORKER_URL);
        }
    }
}
