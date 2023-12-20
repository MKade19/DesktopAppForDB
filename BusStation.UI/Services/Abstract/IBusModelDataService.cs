using BusStation.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IBusModelDataService : IDataService<BusModel>
    {
        Task<IEnumerable<BusModelWithDistance>> GetWithTotalDistanceAsync();
    }
}
