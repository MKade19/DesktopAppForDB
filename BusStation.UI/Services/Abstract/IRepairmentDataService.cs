using BusStation.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IRepairmentDataService : IDataService<Repairment>
    {
        public Task<IEnumerable<Repairment>> GetByBusNumberAsync(string number);
        public Task<IEnumerable<RepairmentYearWithCount>> GetYearsWithCountAsync();
    }
}
