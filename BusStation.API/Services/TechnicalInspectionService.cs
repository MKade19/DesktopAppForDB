using BusStation.API.Data;
using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class TechnicalInspectionService : ITechnicalInspectionService
    {
        private ITechnicalInspetionRepository TechnicalInspectionRepository { get; }
        private IBusRepository BusRepository { get; }

        public TechnicalInspectionService(ITechnicalInspetionRepository technicalInspectionRepository, IBusRepository busRepository)
        {
            TechnicalInspectionRepository = technicalInspectionRepository;
            BusRepository = busRepository;
        }
        public async Task CreateOneAsync(TechnicalInspection technicalInspection)
        {
            await ValidateTechnicalInspectionAsync(technicalInspection);
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
            await ValidateTechnicalInspectionAsync(technicalInspection);
            await TechnicalInspectionRepository.UpdateByIdAsync(technicalInspection);
        }

        private async Task ValidateTechnicalInspectionAsync(TechnicalInspection technicalInspection)
        {
            Bus bus = await BusRepository.GetByIdAsync(technicalInspection.BusId);

            if (bus.Id == -1)
            {
                throw new BadRequestException("There is no such a bus!");
            }
        }

        public async Task<IEnumerable<TechnicalInspection>> GetByYearAndAllowanceAsync(int year, bool isAllowed)
        {
            return await TechnicalInspectionRepository.GetByYearAndAllowanceAsync(year, isAllowed);
        }
    }
}
