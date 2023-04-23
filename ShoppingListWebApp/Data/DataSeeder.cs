namespace ShoppingListWebApp.Data
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //seeding data here
            IEnumerable<Category> categories = new List<Category>()
            {
                new Category { Id = 1, Description = "Food"}
            };

            IEnumerable<ShopItem> shopItems = new List<ShopItem>()
            {
                new ShopItem { Id = 1L, Name = "Tomaat", Quantity = 4, CategoryId = 1}
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<ShopItem>().HasData(shopItems);
        }
    }
}
