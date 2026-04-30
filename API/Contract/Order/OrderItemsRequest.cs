namespace API.Contract.Order;

public class OrderItemsRequest
{
      public Guid ItemId { get; set; }
      public string ItemSerial { get; set; } = "";
      public bool IsDelivered { get; set; } = false;

      public byte[]? PdfData { get; set; }
      public string? PdfFileName { get; set; }

}
