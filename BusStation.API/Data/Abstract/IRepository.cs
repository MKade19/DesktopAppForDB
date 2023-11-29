namespace BusStation.API.Data.Abstract
{
    public interface IRepository<T>
    {
        Task CreateOneAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task UpdateByIdAsync(T entity);
        Task DeleteByIdAsync(int id);
    }
}
