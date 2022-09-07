using Models;

namespace Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingList?> ReadAsync(int id);
        Task<IEnumerable<ShoppingList>> ReadAsync();
    }
}