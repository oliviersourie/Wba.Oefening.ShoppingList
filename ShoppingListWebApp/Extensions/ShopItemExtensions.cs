using ShoppingListWebApp.ViewModels;

namespace ShoppingListWebApp.Extensions
{
    public static class ShopItemExtensions
    {
        public static async Task<IEnumerable<ShopItemViewModel>> MapToViewModelAsync
            (this DbSet<ShopItem> shopItems)
        {
            return await shopItems.Select(si => new ShopItemViewModel
                                         {
                                             Id = si.Id,
                                             Name = si.Name,
                                             Quantity = si.Quantity,
                                             UnitPrice = si.UnitPrice,
                                             CategoryName = si.Category.Description
                                         })
                                    .ToListAsync();
        }
    }
}
