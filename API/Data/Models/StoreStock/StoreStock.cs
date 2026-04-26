
namespace API.Data.Models.StoreStock;


public class StoreStock
{
      public Guid Id { get; set; }
      public Guid StoreId { get; set; }
      public Guid ItemId { get; set; }
      public int Quantity { get; set; }
      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }
      public required API.Data.Models.Store.Store Store { get; set; }
      public required API.Data.Models.Item.Item Item { get; set; }



}
