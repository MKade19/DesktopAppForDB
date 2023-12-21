using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface IRepairmentService : IService<Repairment>
    {
        public Task<IEnumerable<Repairment>> GetByBusNumberAsync(string busNumber);
        public Task<IEnumerable<RepairmentYearWithCount>> GetYearsWithCountAsync();
    }
}
