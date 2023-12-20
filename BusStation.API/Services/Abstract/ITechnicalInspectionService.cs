using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface ITechnicalInspectionService : IService<TechnicalInspection>
    {
        public Task<IEnumerable<TechnicalInspection>> GetByYearAndAllowanceAsync(int year, bool isAllowed);
    }
}
