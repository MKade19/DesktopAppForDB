using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        public Task<IEnumerable<Worker>> GetByPosition(string positionTitle);
    }
}
