using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public partial class CustomerController : ControllerBase
{
      [HttpDelete("{id:Guid}")]
      public async Task<ActionResult> DeleteCustomer([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  await _customerService.DeleteCustomer(id, cancellationToken);
                  return Ok("Customer deleted successfully.");
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Delete Customer " + e);
            }
      }
}

