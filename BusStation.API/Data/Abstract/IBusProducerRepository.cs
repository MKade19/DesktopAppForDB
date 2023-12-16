using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IBusProducerRepository : IRepository<BusProducer>
    {
        public Task<BusProducer> GetByTitleAsync(string title);
    }
}
