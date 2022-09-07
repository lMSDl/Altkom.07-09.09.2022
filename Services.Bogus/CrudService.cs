using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class CrudService<T> : ICrudService<T> where T : Entity
    {
        private ICollection<T> Entities { get; }

        public CrudService(BaseFaker<T> faker, int count = 5)
        {
            Entities = faker.Generate(count);
        }

        public Task<T?> ReadAsync(int id)
        {
            var entity = Entities.SingleOrDefault(x => x.Id == id);

            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(Entities.AsEnumerable());
        }

        public Task<T> CreateAsync(T entity)
        {
            entity.Id = Entities.Max(x => x.Id) + 1;
            Entities.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await DeleteAsync(id);
            entity.Id = id;
            Entities.Add(entity);
        }

        public async Task DeleteAsync(int id)
        {
            Entities.Remove(await ReadAsync(id));
        }
    }
}