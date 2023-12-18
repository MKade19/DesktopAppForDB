using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class BusService : IBusService
    {
        private IBusRepository BusRepository { get; }
        private IBusModelRepository BusModelRepository { get; }

        public BusService(IBusRepository busRepository, IBusModelRepository busModelRepository)
        {
            BusRepository = busRepository;
            BusModelRepository = busModelRepository;
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
            await ValidateBusAsync(bus);
            await BusRepository.CreateOneAsync(bus);
        }

        public async Task UpdateByIdAsync(Bus bus)
        {
            await ValidateBusAsync(bus);
            await BusRepository.UpdateByIdAsync(bus);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await BusRepository.DeleteByIdAsync(id);
        }

        private async Task ValidateBusAsync(Bus bus)
        {
            Bus potentialBus = await BusRepository.GetByNumberAsync(bus.StateNumber);
            BusModel busModel = await BusModelRepository.GetByIdAsync(bus.BusModelId);

            if (potentialBus.Id != -1)
            {
                throw new BadRequestException("There is such a bus with this number!");
            }

            if (busModel.Id == -1)
            {
                throw new BadRequestException("There is no such a model!");
            }
        }
    }
}
