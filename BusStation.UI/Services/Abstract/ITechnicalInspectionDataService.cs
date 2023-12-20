using BusStation.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface ITechnicalInspectionDataService : IDataService<TechnicalInspection>
    {
        public Task<IEnumerable<TechnicalInspection>> GetByYearAndAllowanceAsync(int year, bool isAllowed);
    }
}
