using Models;

namespace Services.Interfaces
{
    public interface ICrudService<T> : IRudService<T> where T : Entity
    {
        Task<IEnumerable<T>> ReadAsync();
        Task<T> CreateAsync(T entity);
    }
}