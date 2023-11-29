using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class BusService : IBusService
    {
        private IBusRepository BusRepository { get; }

        public BusService(IBusRepository busRepository)
        {
            BusRepository = busRepository;
        }

        public async Task<IEnumerable<Bus>> GetAllAsync()
        {
            return await BusRepository.GetAllAsync();
        }

        public async Task<Bus> GetByIdAsync(int id)
        {
            return await BusRepository.GetByIdAsync(id);
        }

        public async Task CreateOneAsync(Bus bus)
        {
            await BusRepository.CreateOneAsync(bus);
        }

        public async Task UpdateByIdAsync(Bus bus)
        {
            await BusRepository.UpdateByIdAsync(bus);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await BusRepository.DeleteByIdAsync(id);
        }
    }
}
