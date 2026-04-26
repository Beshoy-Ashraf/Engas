namespace API.Contract.StoreStock;

public class ItemsInStores
{
      public Guid Id { get; set; }
      public Guid StoreId { get; set; }
      public Guid ItemId { get; set; }
      public int Quantity { get; set; }
      public string StoreName { get; set; } = "";
      public string ItemDescription { get; set; } = "";
      public double ItemCurrentPrice { get; set; }
}
