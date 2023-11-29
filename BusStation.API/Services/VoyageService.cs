using BusStation.API.Data.Abstract;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class VoyageService : IVoyageService
    {
        private IVoyageRepository VoyageRepository { get; }

        public VoyageService(IVoyageRepository voyageRepository)
        {
            VoyageRepository = voyageRepository;
        }
        public async Task CreateOneAsync(Voyage voyage)
        {
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
            await VoyageRepository.UpdateByIdAsync(voyage);
        }
    }
}
