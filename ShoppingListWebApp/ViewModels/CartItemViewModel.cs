namespace ShoppingListWebApp.ViewModels
{
    public class CartItemViewModel
    {
        public long Id { get; init; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
