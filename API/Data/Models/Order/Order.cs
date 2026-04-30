using API.Core.Enums;

namespace API.Data.Models.Order;

public class Order
{
      public Guid Id { get; set; }
      public Guid CustomerId { get; set; }
      public Guid StaffId { get; set; }
      public double TotalAmount { get; set; }
      public string PaymentMethod { get; set; } = "";
      public OrderStatus Status { get; set; } = OrderStatus.Pending;
      public List<OrderItem> OrderItems { get; set; } = [];
      public Customer.Customer? Customer { get; set; }
      public Staff.Staff? Staff { get; set; }

      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }

}
