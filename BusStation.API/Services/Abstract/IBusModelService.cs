using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface IBusModelService : IService<BusModel>
    {
        public Task<IEnumerable<BusModelWithDistance>> GetWithTotalDistanceAsync();
    }
}
