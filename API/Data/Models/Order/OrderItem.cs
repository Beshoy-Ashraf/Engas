namespace API.Data.Models.Order;

public class OrderItem
{
      public Guid Id { get; set; }
      public Guid OrderId { get; set; }
      public Guid ItemId { get; set; }
      public Guid StoreId { get; set; }
      public string ItemSerial { get; set; } = "";
      public bool IsDelivered { get; set; } = false;
      public Order? Order { get; set; }
      public Item.Item? Item { get; set; }
      public Store.Store? Store { get; set; }
      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }

      public byte[]? PdfData { get; set; }
      public string? PdfFileName { get; set; }

}
