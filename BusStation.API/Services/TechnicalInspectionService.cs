using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class TechnicalInspectionService : ITechnicalInspectionService
    {
        private ITechnicalInspetionRepository TechnicalInspectionRepository { get; }

        public TechnicalInspectionService(ITechnicalInspetionRepository technicalInspectionRepository)
        {
            TechnicalInspectionRepository = technicalInspectionRepository;
        }
        public async Task CreateOneAsync(TechnicalInspection technicalInspection)
        {
            await TechnicalInspectionRepository.CreateOneAsync(technicalInspection);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await TechnicalInspectionRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<TechnicalInspection>> GetAllAsync()
        {
            return await TechnicalInspectionRepository.GetAllAsync();
        }

        public async Task<TechnicalInspection> GetByIdAsync(int id)
        {
            return await TechnicalInspectionRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(TechnicalInspection technicalInspection)
        {
            await TechnicalInspectionRepository.UpdateByIdAsync(technicalInspection);
        }
    }
}
