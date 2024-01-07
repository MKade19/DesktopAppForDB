using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
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
            ValidateBusRoute(route);
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
            ValidateBusRoute(route);
            await BusRouteRepository.UpdateByIdAsync(route);
        }
        
        private async void ValidateBusRoute(BusRoute route)
        {
            BusRoute potientialBusRoute = await BusRouteRepository.GetByNumberAsync(route.RouteNumber);

            if (potientialBusRoute.Id != -1 && route.Id == -1)
            {
                throw new BadRequestException("Уже существует маршрут с данным номером!");
            }

            if (potientialBusRoute.Id != -1 && route.Id != potientialBusRoute.Id && route.Id != -1)
            {
                throw new BadRequestException("Cуществует другой маршрут с данным номером!");
            }

            if (route.Departure.Equals(route.Destination)) 
            {
                throw new BadRequestException("Точка отправления и точка прибытия должны отличаться!");
            }
        }
    }
}
