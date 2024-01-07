using BusStation.API.Data.Abstract;
using BusStation.API.Exceptions;
using BusStation.API.Services.Abstract;
using BusStation.Common.Models;

namespace BusStation.API.Services
{
    public class BusModelService : IBusModelService
    {
        private IBusModelRepository BusModelRepository { get; }
        private IBusProducerRepository BusProducerRepository { get; }

        public BusModelService(IBusModelRepository busModelRepository, IBusProducerRepository busProducerRepository)
        {
            BusModelRepository = busModelRepository;
            BusProducerRepository = busProducerRepository;
        }

        public Task<IEnumerable<BusModel>> GetAllAsync()
        {
            return BusModelRepository.GetAllAsync();
        }

        public Task<BusModel> GetByIdAsync(int id)
        {
            return BusModelRepository.GetByIdAsync(id);
        }

        public async Task CreateOneAsync(BusModel model)
        {
            await ValidateModelAsync(model);
            await BusModelRepository.CreateOneAsync(model);
        }

        public async Task UpdateByIdAsync(BusModel model)
        {
            await ValidateModelAsync(model);
            await BusModelRepository.UpdateByIdAsync(model);
        }

        public Task DeleteByIdAsync(int id)
        {
            return BusModelRepository.DeleteByIdAsync(id);
        }

        private async Task ValidateModelAsync(BusModel model)
        {
            BusModel potentialModel = await BusModelRepository.GetByTitleAsync(model.Title);
            BusProducer busProducer = await BusProducerRepository.GetByIdAsync(model.ProducerId);

            if (potentialModel.Id != -1 && model.Id == -1)
            {
                throw new BadRequestException("Уже существует модель с данным наименованием!");
            }

            if (potentialModel.Id != model.Id && potentialModel.Id != -1 && model.Id != -1)
            {
                throw new BadRequestException("Существует другая модель с данным наименованием!");
            }

            if (busProducer.Id == -1)
            {
                throw new BadRequestException("Данного производителя не существует!");
            }
        }

        public async Task<IEnumerable<BusModelWithDistance>> GetWithTotalDistanceAsync()
        {
            return await BusModelRepository.GetWithTotalDistanceAsync();
        }
    }
}
