using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IVoyageRepository : IRepository<Voyage>
    {
        public Task<IEnumerable<Voyage>> GetByRouteNumberAsync(string routeNumber);
    }
}
