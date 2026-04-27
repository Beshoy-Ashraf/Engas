using API.Contract.Customer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public partial class CustomerController : ControllerBase
{
      [HttpPut("{id:Guid}")]
      public async Task<ActionResult<Guid>> UpdateCustomer([FromRoute] Guid id, [FromBody] CustomerRequest request, CancellationToken cancellationToken)
      {
            try
            {
                  var updatedId = await _customerService.UpdateCustomerData(id, request, cancellationToken);
                  return Ok(updatedId);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Update Customer " + e);
            }
      }
}

