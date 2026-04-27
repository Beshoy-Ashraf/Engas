using API.Contract.Customer;
using API.Data.Models.Customer;
using API.Services.CustomerService.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Services.CustomerService;

public class CustomerService(AppDBContext dbContext, ILogger<CustomerService> logger) : ICustomerService
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly ILogger<ICustomerService> _logger = logger;

      public async Task<Guid> AddCustomer(CustomerRequest NewCustomer, CancellationToken cancellationToken)
      {
            var customer = new Customer
            {
                  Name = NewCustomer.Name,
                  Email = NewCustomer.Email,
                  PhoneNumber = NewCustomer.PhoneNumber,
                  City = NewCustomer.City,
                  Address = NewCustomer.Address,
                  TotalPaidAmount = NewCustomer.TotalPaidAmount,
                  PdfData = NewCustomer.PdfData,
                  PdfFileName = NewCustomer.PdfFileName,
                  CreatedDate = DateTime.UtcNow
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return customer.Id;

      }
      public async Task DeleteCustomer(Guid id, CancellationToken cancellationToken)
      {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Customer with ID {id} was not found.");
            customer.DeletedDate = DateTime.UtcNow;
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
      }
      public async Task<List<CustomerResponse>> GetCustomers(CancellationToken cancellationToken)
      {
            var customers = await _dbContext.Customers.Where(x => x.DeletedDate == null).ToListAsync(cancellationToken: cancellationToken);
            return customers.Select(x => new CustomerResponse
            {
                  Id = x.Id,
                  Name = x.Name,
                  Email = x.Email,
                  PhoneNumber = x.PhoneNumber,

                  City = x.City,
                  Address = x.Address,
                  TotalPaidAmount = x.TotalPaidAmount,
                  HasPdf = x.PdfData != null && x.PdfData.Length > 0,
                  PdfFileName = x.PdfFileName
            }).ToList();
      }
      public async Task<CustomerResponse> GetCustomer(Guid id, CancellationToken cancellationToken)
      {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Customer with ID {id} was not found.");
            return new CustomerResponse
            {
                  Id = customer.Id,
                  Name = customer.Name,
                  Email = customer.Email,
                  PhoneNumber = customer.PhoneNumber,
                  City = customer.City,
                  Address = customer.Address,
                  TotalPaidAmount = customer.TotalPaidAmount,
                  HasPdf = customer.PdfData != null && customer.PdfData.Length > 0,
                  PdfFileName = customer.PdfFileName
            };
      }
      public async Task<Guid> UpdateCustomerData(Guid id, CustomerRequest CustomerData, CancellationToken cancellationToken)
      {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Customer with ID {id} was not found.");
            customer.Name = CustomerData.Name;
            customer.Email = CustomerData.Email;
            customer.PhoneNumber = CustomerData.PhoneNumber;
            customer.City = CustomerData.City;
            customer.Address = CustomerData.Address;
            customer.TotalPaidAmount = CustomerData.TotalPaidAmount;
            customer.UpdatedDate = DateTime.UtcNow;

            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return customer.Id;
      }

      public async Task UploadCustomerPdf(Guid id, byte[] pdfData, string fileName, CancellationToken cancellationToken)
      {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken)
                  ?? throw new KeyNotFoundException($"Customer with ID {id} was not found.");

            customer.PdfData = pdfData;
            customer.PdfFileName = fileName;
            customer.UpdatedDate = DateTime.UtcNow;

            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
      }

      public async Task<(byte[] PdfData, string FileName)?> GetCustomerPdf(Guid id, CancellationToken cancellationToken)
      {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken)
                  ?? throw new KeyNotFoundException($"Customer with ID {id} was not found.");

            if (customer.PdfData == null || customer.PdfData.Length == 0)
                  return null;

            return (customer.PdfData, customer.PdfFileName ?? "customer.pdf");
      }

}
