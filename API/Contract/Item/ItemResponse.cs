using API.Core;

namespace API.Contract.Item;

public class ItemResponse
{
      public Guid Id { get; set; }
      public string Brand { get; set; } = "";
      public string Model { get; set; } = "";
      public string Description { get; set; } = "";
      public double OldPrice { get; set; } = 0;
      public double CurrentPrice { get; set; } = 0;
      public StockLocation StockLocation { get; set; }

}
