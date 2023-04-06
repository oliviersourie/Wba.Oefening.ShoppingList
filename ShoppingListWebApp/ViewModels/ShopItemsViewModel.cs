namespace ShoppingListWebApp.ViewModels
{
    public class ShopItemsViewModel
    {
        public ShopItemViewModel ShopItem { get; set; } // voor het formulier
        public IEnumerable<ShopItemViewModel> ShopItems { get; set; } // voor de lijst van shop items
    }
}