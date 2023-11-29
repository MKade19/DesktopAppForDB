namespace BusStation.API.Services.Abstract
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateOneAsync(T entity);
        Task UpdateByIdAsync(T entity);
        Task DeleteByIdAsync(int id);
    }
}
