using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IBusRouteRepository : IRepository<BusRoute>
    {
        public Task<BusRoute> GetByNumberAsync(string routeNumber);
    }
}
