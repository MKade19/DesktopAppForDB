using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class TechnicalInspectionDataService : HttpDataServiceBase, ITechnicalInspectionDataService
    {
        private const string TECHNICAL_INSPECTION_URL = "technical-inspections";

        public async Task CreateOneAsync(TechnicalInspection technicalInspection)
        {
            JsonContent content = JsonContent.Create(technicalInspection);
            await PostAsync(content, TECHNICAL_INSPECTION_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(TECHNICAL_INSPECTION_URL + "/" + id);
        }

        public async Task<IEnumerable<TechnicalInspection>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<TechnicalInspection>>(await GetAsync(TECHNICAL_INSPECTION_URL)) ?? new List<TechnicalInspection>();
        }

        public async Task<TechnicalInspection> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<TechnicalInspection>(await GetAsync(TECHNICAL_INSPECTION_URL + "/" + id)) ?? new TechnicalInspection();
        }

        public async Task UpdateByIdAsync(TechnicalInspection technicalInspection)
        {
            JsonContent content = JsonContent.Create(technicalInspection);
            await PutAsync(content, TECHNICAL_INSPECTION_URL);
        }
    }
}
