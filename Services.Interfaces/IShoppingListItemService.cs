using Models;

namespace Services.Interfaces
{
    public interface IShoppingListItemService : IRudService<ShoppingListItem>
    {
        Task<IEnumerable<ShoppingListItem>> ReadCollectionAsync(int parentId);
        Task<ShoppingListItem> CreateAsync(int parentId, ShoppingListItem entity);
    }
}