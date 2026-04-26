using API.Contract.Staff;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public partial class StaffController : ControllerBase
{

      [HttpDelete("{id:Guid}")]
      public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  await _services.DeleteStaff(id, cancellationToken);
                  return Ok();
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Add Staff " + e);
            }
      }
}
