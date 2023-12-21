using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class VoyageService : IVoyageService
    {
        private IVoyageRepository VoyageRepository { get; }
        private IBusRepository BusRepository { get; }
        private IWorkerRepository WorkerRepository { get; }
        private IBusRouteRepository BusRouteRepository { get; }

        public VoyageService(IVoyageRepository voyageRepository, IBusRepository busRepository, IWorkerRepository workerRepository, IBusRouteRepository busRouteRepository)
        {
            VoyageRepository = voyageRepository;
            BusRepository = busRepository;
            WorkerRepository = workerRepository;
            BusRouteRepository = busRouteRepository;
        }
        public async Task CreateOneAsync(Voyage voyage)
        {
            await ValidateVoyage(voyage);
            await VoyageRepository.CreateOneAsync(voyage);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await VoyageRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<Voyage>> GetAllAsync()
        {
            return await VoyageRepository.GetAllAsync();
        }

        public async Task<Voyage> GetByIdAsync(int id)
        {
            return await VoyageRepository.GetByIdAsync(id);
        }

        public async Task UpdateByIdAsync(Voyage voyage)
        {
            await ValidateVoyage(voyage);
            await VoyageRepository.UpdateByIdAsync(voyage);
        }

        public async Task ValidateVoyage(Voyage voyage)
        {
            Bus bus = await BusRepository.GetByIdAsync(voyage.BusId);
            Worker worker = await WorkerRepository.GetByIdAsync(voyage.WorkerId);
            BusRoute busRoute = await BusRouteRepository.GetByIdAsync(voyage.BusRouteId);

            if (bus.Id == -1)
            {
                throw new BadRequestException("There is no such a bus!");
            }
            
            if (worker.Id == -1)
            {
                throw new BadRequestException("There is no such a worker!");
            }
            
            if (busRoute.Id == -1)
            {
                throw new BadRequestException("There is no such a bus route!");
            }

            if (voyage.DepartureTime >= voyage.ArrivalTime) 
            {
                throw new BadRequestException("DepartureTime must be less than ArrivalTime!");
            }
        }

        public async Task<IEnumerable<Voyage>> GetByRouteNumberAsync(string routeNumber)
        {
            return await VoyageRepository.GetByRouteNumberAsync(routeNumber);
        }
    }
}
