using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class RepairmentService : IRepairmentService
    {
        private IRepairmentRepository RepairmentRepository { get; }

        public RepairmentService(IRepairmentRepository repairmentRepository)
        {
            RepairmentRepository = repairmentRepository;
        }
        public async Task CreateOneAsync(Repairment repairment)
        {
            await RepairmentRepository.CreateOneAsync(repairment);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await RepairmentRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<Repairment>> GetAllAsync()
        {
            return await RepairmentRepository.GetAllAsync();
        }

        public async Task<Repairment> GetByIdAsync(int id)
        {
            return await RepairmentRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(Repairment repairment)
        {
            await RepairmentRepository.UpdateByIdAsync(repairment);
        }
    }
}
