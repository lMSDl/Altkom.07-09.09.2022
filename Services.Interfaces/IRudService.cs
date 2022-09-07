using Models;

namespace Services.Interfaces
{
    public interface IRudService<T> where T : Entity
    {
        Task<T?> ReadAsync(int id);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}