using BusStation.Common.Models;
using BusStation.UI.Services.Abstract;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusStation.UI.Services
{
    public class MedicalInspectionDataService : HttpDataServiceBase, IMedicalInspectionDataService
    {
        public const string MEDICAL_INSPECTION_URL = "medical-inspections";

        public async Task CreateOneAsync(MedicalInspection medicalInspection)
        {
            JsonContent content = JsonContent.Create(medicalInspection);
            await PostAsync(content, MEDICAL_INSPECTION_URL);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteAsync(MEDICAL_INSPECTION_URL + "/" + id);
        }

        public async Task<IEnumerable<MedicalInspection>> GetAllAsync()
        {
            return JsonSerializer.Deserialize<List<MedicalInspection>>(await GetAsync(MEDICAL_INSPECTION_URL)) ?? new List<MedicalInspection>();
        }

        public async Task<MedicalInspection> GetByIdAsync(int id)
        {
            return JsonSerializer.Deserialize<MedicalInspection>(await GetAsync(MEDICAL_INSPECTION_URL + "/" + id)) ?? new MedicalInspection();
        }

        public async Task UpdateByIdAsync(MedicalInspection medicalInspection)
        {
            JsonContent content = JsonContent.Create(medicalInspection);
            await PutAsync(content, MEDICAL_INSPECTION_URL);
        }
    }
}
