using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class BusModelService : IBusModelService
    {
        private IBusModelRepository BusModelRepository { get; }

        public BusModelService(IBusModelRepository busModelRepository)
        {
            BusModelRepository = busModelRepository;
        }

        public Task<IEnumerable<BusModel>> GetAllAsync()
        {
            return BusModelRepository.GetAllAsync();
        }

        public Task<BusModel> GetByIdAsync(int id)
        {
            return BusModelRepository.GetByIdAsync(id);
        }

        public Task CreateOneAsync(BusModel model)
        {
            return BusModelRepository.CreateOneAsync(model);
        }

        public Task UpdateByIdAsync(BusModel model)
        {
            return BusModelRepository.UpdateByIdAsync(model);
        }

        public Task DeleteByIdAsync(int id)
        {
            return BusModelRepository.DeleteByIdAsync(id);
        }
    }
}
