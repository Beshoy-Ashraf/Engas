namespace API.Contract.Order;

public class OrderItemResponse
{
      public Guid Id { get; set; }
      public Guid OrderId { get; set; }
      public Guid StoreId { get; set; }
      public Guid ItemId { get; set; }
      public string ItemSerial { get; set; } = "";
      public string ItemBrand { get; set; } = "";
      public string ItemModel { get; set; } = "";
      public string ItemDescription { get; set; } = "";
      public bool IsDelivered { get; set; } = false;
      public double ItemPrice { get; set; }
      public string StoreName { get; set; } = "";
      public string StoreCity { get; set; } = "";
      public string StoreCode { get; set; } = "";
      public string OrderDate { get; set; } = "";
      public string CreatedDate { get; set; } = "";
      public string UpdatedDate { get; set; } = "";
      public bool HasPdf { get; set; }
      public string? PdfFileName { get; set; }


}
