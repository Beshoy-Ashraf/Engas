namespace API.Contract.Customer;

public class CustomerResponse
{
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
      public string Email { get; set; } = "";
      public string PhoneNumber { get; set; } = "";
      public string City { get; set; } = "";
      public string Address { get; set; } = "";
      public double TotalPaidAmount { get; set; } = 0.0;
      public bool HasPdf { get; set; }
      public string? PdfFileName { get; set; }
}
