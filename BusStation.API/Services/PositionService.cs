using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class PositionService : IPositionService
    {
        private IPositionRepository PositionRepository { get; }

        public PositionService(IPositionRepository positionRepository)
        {
            PositionRepository = positionRepository;
        }

        public async Task CreateOneAsync(Position position)
        {
            await PositionRepository.CreateOneAsync(position);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await PositionRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return await PositionRepository.GetAllAsync();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return await PositionRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(Position position)
        {
            await PositionRepository.UpdateByIdAsync(position);
        }
    }
}
