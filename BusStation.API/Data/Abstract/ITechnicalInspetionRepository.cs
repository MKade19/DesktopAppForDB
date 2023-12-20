using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface ITechnicalInspetionRepository : IRepository<TechnicalInspection>
    {
        public Task<IEnumerable<TechnicalInspection>> GetByYearAndAllowanceAsync(int year, bool isAllowed);
    }
}
