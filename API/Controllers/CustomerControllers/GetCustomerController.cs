using API.Contract.Customer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public partial class CustomerController : ControllerBase
{
      [HttpGet("{id:Guid}")]
      public async Task<ActionResult<CustomerResponse>> GetCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  var customer = await _customerService.GetCustomer(id, cancellationToken);
                  return Ok(customer);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Customer " + e);
            }
      }
      [HttpGet]
      public async Task<ActionResult<List<CustomerResponse>>> GetCustomers(CancellationToken cancellationToken)
      {
            try
            {
                  var customers = await _customerService.GetCustomers(cancellationToken);
                  return Ok(customers);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Customers " + e);
            }
      }

      [HttpGet("{id:Guid}/pdf")]
      public async Task<IActionResult> GetCustomerPdf([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  var result = await _customerService.GetCustomerPdf(id, cancellationToken);
                  if (result == null)
                        return NotFound("No PDF found for this customer.");

                  return File(result.Value.PdfData, "application/pdf", result.Value.FileName);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Customer PDF " + e);
            }
      }
}

