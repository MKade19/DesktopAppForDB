using BusStation.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IMedicalInspectionDataService : IDataService<MedicalInspection>
    {
        public Task<IEnumerable<MedicalInspection>> GetAllDescAsync();
        public Task<IEnumerable<MedicalInspection>> GetAllAscAsync();
    }
}
