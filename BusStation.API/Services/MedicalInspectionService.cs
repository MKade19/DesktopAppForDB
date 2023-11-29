using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class MedicalInspectionService : IMedicalInspectionService
    {
        private IMedicalInspetionRepository MedicalInspetionRepository { get; }

        public MedicalInspectionService(IMedicalInspetionRepository medicalInspectionRepository)
        {
            MedicalInspetionRepository = medicalInspectionRepository;
        }
        public async Task CreateOneAsync(MedicalInspection medicalInspection)
        {
            await MedicalInspetionRepository.CreateOneAsync(medicalInspection);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await MedicalInspetionRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<MedicalInspection>> GetAllAsync()
        {
            return await MedicalInspetionRepository.GetAllAsync();
        }

        public async Task<MedicalInspection> GetByIdAsync(int id)
        {
            return await MedicalInspetionRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(MedicalInspection medicalInspection)
        {
            await MedicalInspetionRepository.UpdateByIdAsync(medicalInspection);
        }
    }
}
