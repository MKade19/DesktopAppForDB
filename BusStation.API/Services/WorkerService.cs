using BusStation.API.Data;
using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class WorkerService : IWorkerService
    {  
        private IWorkerRepository WorkerRepository { get; }
        private IPositionRepository PositionRepository { get; }

        public WorkerService(IWorkerRepository workerRepository, IPositionRepository positionRepository)
        {
            WorkerRepository = workerRepository;
            PositionRepository = positionRepository;
        }
        public async Task CreateOneAsync(Worker worker)
        {
            await ValidateWorkerAsync(worker);
            await WorkerRepository.CreateOneAsync(worker);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await WorkerRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await WorkerRepository.GetAllAsync();
        }

        public async Task<Worker> GetByIdAsync(int id)
        {
            return await WorkerRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(Worker worker)
        {
            await ValidateWorkerAsync(worker);
            await WorkerRepository.UpdateByIdAsync(worker);
        }

        public async Task<IEnumerable<Worker>> GetByPosition(string positionTitle)
        {
            return await WorkerRepository.GetByPosition(positionTitle);
        }
        private async Task ValidateWorkerAsync(Worker worker)
        {
            Worker potentialWorker = await WorkerRepository.GetByNameAsync(worker.Fullname);
            Position position = await PositionRepository.GetByIdAsync(worker.PositionId);

            if (potentialWorker.Id != -1 && worker.Id == -1)
            {
                throw new BadRequestException("Уже существует сотрудник с данным ФИО!");
            }

            if (potentialWorker.Id != -1 && potentialWorker.Id != worker.Id && worker.Id != -1)
            {
                throw new BadRequestException("Cуществует другой сотрудник с данным ФИО!");
            }

            if (position.Id == -1)
            {
                throw new BadRequestException("Данная должность не существует!");
            }
        }
    }
}
