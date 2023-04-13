namespace ShoppingListWebApp.Data
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //seeding data here
            IEnumerable<ShopItem> shopItems = new List<ShopItem>()
            {
                new ShopItem { Id = 1L, Name = "Tomaat", Quantity = 4},
            };

            modelBuilder.Entity<ShopItem>().HasData(shopItems);
        }
    }
}
