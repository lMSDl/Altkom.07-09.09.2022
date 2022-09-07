using Models;
using Services.Bogus.Fakers;
using Services.Interfaces;

namespace Services.Bogus
{
    public class ShoppingListItemService : IShoppingListItemService
    {
        private ICollection<ShoppingListItem> Entities { get; }
        //private ICollection<ShoppingList> Entities { get; } = new List<ShoppingList> { new ShoppingList { Id = 10, Name = "123123", DateTime = DateTime.Now } };


        public ShoppingListItemService(ShoppingListItemFaker faker, int count = 30)
        {
            Entities = faker.Generate(count);
        }

        public Task<ShoppingListItem?> ReadAsync(int id)
        {
            var entity = Entities.SingleOrDefault(x => x.Id == id);

            return Task.FromResult(entity);
        }

        public Task<IEnumerable<ShoppingListItem>> ReadCollectionAsync(int parentId)
        {
            return Task.FromResult(Entities.Where(x => x.ShoppingListId == parentId).AsEnumerable());
        }

        public Task<ShoppingListItem> CreateAsync(int parentId, ShoppingListItem entity)
        {
            entity.Id = Entities.Max(x => x.Id) + 1;
            entity.ShoppingListId = parentId;
            Entities.Add(entity);

            return Task.FromResult(entity);
        }

        public async Task UpdateAsync(int id, ShoppingListItem entity)
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