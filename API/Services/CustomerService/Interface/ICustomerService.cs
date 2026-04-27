using API.Contract.Customer;

namespace API.Services.CustomerService.Interface;

public interface ICustomerService
{

      Task<List<CustomerResponse>> GetCustomers(CancellationToken cancellationToken);
      Task<CustomerResponse> GetCustomer(Guid id, CancellationToken cancellationToken);
      Task<Guid> UpdateCustomerData(Guid id, CustomerRequest CustomerData, CancellationToken cancellationToken);
      Task<Guid> AddCustomer(CustomerRequest NewCustomer, CancellationToken cancellationToken);
      Task DeleteCustomer(Guid id, CancellationToken cancellationToken);
      Task UploadCustomerPdf(Guid id, byte[] pdfData, string fileName, CancellationToken cancellationToken);
      Task<(byte[] PdfData, string FileName)?> GetCustomerPdf(Guid id, CancellationToken cancellationToken);
}
