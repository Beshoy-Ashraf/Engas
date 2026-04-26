namespace API.Contract.StoreStock;

public class StoreStockRequest
{
      public Guid StoreId { get; set; }
      public Guid ItemId { get; set; }
      public int Quantity { get; set; }

}
