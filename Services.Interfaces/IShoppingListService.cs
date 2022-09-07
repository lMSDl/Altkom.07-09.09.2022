using Models;

namespace Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingList?> ReadAsync(int id);
        Task<IEnumerable<ShoppingList>> ReadAsync();
        Task<ShoppingList> CreateAsync(ShoppingList entity);
        Task UpdateAsync(int id, ShoppingList entity);
        Task DeleteAsync(int id);
    }
}