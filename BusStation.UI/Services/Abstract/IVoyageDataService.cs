using BusStation.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IVoyageDataService : IDataService<Voyage>
    {
        public Task<IEnumerable<Voyage>> GetByRouteNumberAsync(string routeNumber);
    }
}
