namespace ShoppingListWebApp.Data
{
    public class ShoppingListContext: DbContext
    {
        public DbSet<ShopItem> ShopItems { get; set; }

        public ShoppingListContext(DbContextOptions<ShoppingListContext> options) : base(options)
        {
        }
    }
}
