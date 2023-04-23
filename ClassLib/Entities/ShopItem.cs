namespace ClassLib.Entities;

public class ShopItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

