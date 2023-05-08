using System.Text.Json.Serialization;

namespace ClassLib.Entities;

public class ShopItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    [JsonIgnore]
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category Category { get; set; }
}

