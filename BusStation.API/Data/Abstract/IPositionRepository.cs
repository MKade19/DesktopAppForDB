using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IPositionRepository : IRepository<Position>
    {
        public Task<Position> GetByTitleAsync(string title);
    }
}
