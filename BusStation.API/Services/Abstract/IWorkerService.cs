using BusStation.Common.Models;

namespace BusStation.API.Services.Abstract
{
    public interface IWorkerService : IService<Worker>
    {
        public Task<IEnumerable<Worker>> GetByPosition(string positionTitle);
    }
}
