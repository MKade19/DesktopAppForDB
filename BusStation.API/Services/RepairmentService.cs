using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class RepairmentService : IRepairmentService
    {
        private IRepairmentRepository RepairmentRepository { get; }
        private IBusRepository BusRepository { get; }
        private IWorkerRepository WorkerRepository { get; }

        public RepairmentService(IRepairmentRepository repairmentRepository, IBusRepository busRepository, IWorkerRepository workerRepository)
        {
            RepairmentRepository = repairmentRepository;
            BusRepository = busRepository;
            WorkerRepository = workerRepository;
        }
        public async Task CreateOneAsync(Repairment repairment)
        {
            await ValidateRepairment(repairment);
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
            await ValidateRepairment(repairment);
            await RepairmentRepository.UpdateByIdAsync(repairment);
        }

        public async Task ValidateRepairment(Repairment repairment)
        {
            Bus bus = await BusRepository.GetByIdAsync(repairment.BusId);
            Worker worker = await WorkerRepository.GetByIdAsync(repairment.WorkerId);

            if (bus.Id == -1)
            {
                throw new BadRequestException("There is no such a bus!");
            }

            if (worker.Id == -1)
            {
                throw new BadRequestException("There is no such a worker!");
            }

            if (repairment.BeginDate >= repairment.EndDate)
            {
                throw new BadRequestException("BeginDate must be less than EndDate!");
            }
        }
    }
}
