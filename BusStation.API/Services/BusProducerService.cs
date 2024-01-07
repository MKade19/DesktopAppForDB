using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class BusProducerService : IBusProducerService
    {
        private IBusProducerRepository BusProducerRepository { get; }

        public BusProducerService(IBusProducerRepository busProducerRepository)
        {
            BusProducerRepository = busProducerRepository;
        }

        public async Task<IEnumerable<BusProducer>> GetAllAsync()
        {
            return await BusProducerRepository.GetAllAsync();
        }

        public async Task<BusProducer> GetByIdAsync(int id)
        {
            return await BusProducerRepository.GetByIdAsync(id);
        }

        public async Task CreateOneAsync(BusProducer producer)
        {
            await ValidateProducerAsync(producer);
            await BusProducerRepository.CreateOneAsync(producer);
        }

        public async Task UpdateByIdAsync(BusProducer producer)
        {
            await ValidateProducerAsync(producer);
            await BusProducerRepository.UpdateByIdAsync(producer);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await BusProducerRepository.DeleteByIdAsync(id);
        }

        private async Task ValidateProducerAsync(BusProducer producer)
        {
            BusProducer potentialProducer = await BusProducerRepository.GetByTitleAsync(producer.Title);

            if (potentialProducer.Id != -1 && producer.Id == -1)
            {
                throw new BadRequestException("Уже существует производитель с данным наименованием!");
            }

            if (potentialProducer.Id != -1 && producer.Id != potentialProducer.Id && producer.Id != -1)
            {
                throw new BadRequestException("Cуществует другой производитель с данным наименованием!");
            }
        }
    }
}
