using API.Core.Enums;

namespace API.Contract.Order;

public class OrderRequest
{
      public Guid CustomerId { get; set; }
      public Guid StaffId { get; set; }
      public double TotalAmount { get; set; }
      public string PaymentMethod { get; set; } = "";
      public OrderStatus Status { get; set; } = OrderStatus.Pending;
      public List<OrderItemsRequest> OrderItems { get; set; } = [];


}
