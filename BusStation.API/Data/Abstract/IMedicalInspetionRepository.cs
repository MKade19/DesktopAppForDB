using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IMedicalInspetionRepository :IRepository<MedicalInspection>
    {
        public Task<IEnumerable<MedicalInspection>> GetAllAscAsync();
        public Task<IEnumerable<MedicalInspection>> GetAllDescAsync();
    }
}
