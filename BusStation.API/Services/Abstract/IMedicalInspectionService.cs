using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface IMedicalInspectionService : IService<MedicalInspection>
    {
        public Task<IEnumerable<MedicalInspection>> GetAllAscAsync();
        public Task<IEnumerable<MedicalInspection>> GetAllDescAsync();
    }
}
