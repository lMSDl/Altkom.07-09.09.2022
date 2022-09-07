using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class ShoppingListService : IShoppingListService
    {
        private ICollection<ShoppingList> Entities { get; }

        public ShoppingListService(ShoppingListFaker faker, int count = 5)
        {
            Entities = faker.Generate(count);
        }

        public Task<ShoppingList?> ReadAsync(int id)
        {
            var entity = Entities.SingleOrDefault(x => x.Id == id);

            return Task.FromResult(entity);
        }

        public Task<IEnumerable<ShoppingList>> ReadAsync()
        {
            return Task.FromResult(Entities.AsEnumerable());
        }

        public Task<ShoppingList> CreateAsync(ShoppingList entity)
        {
            entity.Id = Entities.Max(x => x.Id) + 1;
            Entities.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task UpdateAsync(int id, ShoppingList entity)
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