using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IBusModelRepository : IRepository<BusModel>
    {
        public Task<BusModel> GetByTitleAsync(string title);

        public Task<IEnumerable<BusModelWithDistance>> GetWithTotalDistanceAsync();
    }
}
