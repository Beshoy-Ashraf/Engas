namespace API.Contract.Customer;

public class CustomerRequest
{
      public string Name { get; set; } = "";
      public string Email { get; set; } = "";
      public string PhoneNumber { get; set; } = "";
      public string City { get; set; } = "";
      public string Address { get; set; } = "";
      public double TotalPaidAmount { get; set; } = 0.0;
      public byte[]? PdfData { get; set; }
      public string? PdfFileName { get; set; }
}
