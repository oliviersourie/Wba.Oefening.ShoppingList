namespace ClassLib.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<ShopItem> ShopItems { get; set; }
    }
}