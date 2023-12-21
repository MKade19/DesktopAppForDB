using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface IBusService : IService<Bus>
    {
        public Task<IEnumerable<BusColorWithCount>> GetColorsWithCount();
    }
}
