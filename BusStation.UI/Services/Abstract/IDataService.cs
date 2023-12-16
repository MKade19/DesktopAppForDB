using BusStation.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        //Task<T> CreateOneAsync(T entity);        
        Task CreateOneAsync(T entity);
        //Task<T> UpdateByIdAsync(T entity);
        Task UpdateByIdAsync(T entity);
        //Task<T> DeleteByIdAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}
