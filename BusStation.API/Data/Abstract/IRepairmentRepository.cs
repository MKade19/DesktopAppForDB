using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IRepairmentRepository : IRepository<Repairment>
    {
        public Task<IEnumerable<Repairment>> GetByBusNumberAsync(string busNumber);
        public Task<IEnumerable<RepairmentYearWithCount>> GetYearsWithCountAsync();
    }
}
