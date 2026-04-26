using API.Core;
using API.Data.Models.StoreStock;

namespace API.Data.Models.Item;

public class Item
{
      public Guid Id { get; set; }
      public string Brand { get; set; } = "";
      public string Model { get; set; } = "";
      public string Description { get; set; } = "";
      public double OldPrice { get; set; } = 0;
      public double CurrentPrice { get; set; } = 0;
      public StockLocation StockLocation { get; set; }
      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }
      public List<StoreStockLevel> StoreStocks { get; set; } = [];

}
