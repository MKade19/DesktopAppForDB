using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface IVoyageService : IService<Voyage>
    {
        public Task<IEnumerable<Voyage>> GetByRouteNumberAsync(string routeNumber);
    }
}
