using BusStation.Common.Models;

namespace BusStation.API.Data.Abstract
{
    public interface IBusRepository : IRepository<Bus>
    {
        public Task<Bus> GetByNumberAsync(string busNumber);
    }
}
