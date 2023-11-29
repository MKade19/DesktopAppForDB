using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class BusRouteService : IBusRouteService
    {
        private IBusRouteRepository BusRouteRepository { get; }

        public BusRouteService(IBusRouteRepository busRouteRepository) { 
            BusRouteRepository = busRouteRepository;
        }

        public async Task CreateOneAsync(BusRoute route)
        {
            await BusRouteRepository.CreateOneAsync(route);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await BusRouteRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<BusRoute>> GetAllAsync()
        {
            return await BusRouteRepository.GetAllAsync();
        }

        public async Task<BusRoute> GetByIdAsync(int id)
        {
            return await BusRouteRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(BusRoute route)
        {
            await BusRouteRepository.UpdateByIdAsync(route);
        }
    }
}
