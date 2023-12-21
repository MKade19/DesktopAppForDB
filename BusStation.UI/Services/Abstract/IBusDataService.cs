using BusStation.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IBusDataService : IDataService<Bus>
    {
        public Task<IEnumerable<BusColorWithCount>> GetColorsWithCountAsync();
    }
}
