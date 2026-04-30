using API.Data.Models.StoreStock;

namespace API.Data.Models.Store;

public class Store
{
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public string City { get; set; } = "";
      public required string Code { get; set; }
      public required string Password { get; set; }
      public List<StoreStockLevel> StoreStockLevel { get; set; } = [];
      public List<Order.OrderItem> OrderItems { get; set; } = [];

      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }


}
