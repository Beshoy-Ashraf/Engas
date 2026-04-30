namespace API.Contract.Order;

public class OrderResponse
{
      public Guid Id { get; set; }
      public Guid CustomerId { get; set; }
      public Guid StaffId { get; set; }
      public string StaffName { get; set; } = "";
      public double TotalAmount { get; set; }
      public string PaymentMethod { get; set; } = "";
      public string Status { get; set; } = "";
      public List<OrderItemResponse> OrderItems { get; set; } = [];
      public string CreatedDate { get; set; } = "";
      public string UpdatedDate { get; set; } = "";

}
