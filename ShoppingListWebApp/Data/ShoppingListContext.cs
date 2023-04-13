namespace ShoppingListWebApp.Data
{
    public class ShoppingListContext: DbContext
    {
        public DbSet<ShopItem> ShopItems { get; set; }

        public ShoppingListContext(DbContextOptions<ShoppingListContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopItem>()
                        .Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(20);

            DataSeeder.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
