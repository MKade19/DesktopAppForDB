using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class WorkerService : IWorkerService
    {  
        private IWorkerRepository WorkerRepository { get; }

        public WorkerService(IWorkerRepository busProducerRepository)
        {
            WorkerRepository = busProducerRepository;
        }
        public async Task CreateOneAsync(Worker worker)
        {
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

        public async Task UpdateByIdAsync(Worker entity)
        {
            await WorkerRepository.UpdateByIdAsync(entity);
        }
    }
}
