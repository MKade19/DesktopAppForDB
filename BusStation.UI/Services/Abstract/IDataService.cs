using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);     
        Task CreateOneAsync(T entity);
        Task UpdateByIdAsync(T entity);
        Task DeleteByIdAsync(int id);
    }
}
