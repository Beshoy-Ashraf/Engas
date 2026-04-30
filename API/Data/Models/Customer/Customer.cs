namespace API.Data.Models.Customer;

public class Customer
{
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
      public string Email { get; set; } = "";
      public string PhoneNumber { get; set; } = "";
      public string City { get; set; } = "";
      public string Address { get; set; } = "";
      public double TotalPaidAmount { get; set; } = 0.0;
      public List<Order.Order> Orders { get; set; } = [];
      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }
      public byte[]? PdfData { get; set; }
      public string? PdfFileName { get; set; }

}
