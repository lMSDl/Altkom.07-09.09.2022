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
    }
}