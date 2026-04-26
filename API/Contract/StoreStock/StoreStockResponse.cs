namespace API.Contract.StoreStock;

public class StoreStockResponse
{

      public Guid Id { get; set; }
      public Guid StoreId { get; set; }
      public Guid ItemId { get; set; }
      public int Quantity { get; set; }
      public double ItemCurrentPrice { get; set; }
}
