using API.Contract.Staff;
using Microsoft.AspNetCore.Mvc;

public partial class StaffController : ControllerBase
{

      [HttpPost]
      public async Task<ActionResult<Guid>> AddStaff([FromBody] AddStaff addStaff, CancellationToken cancellationToken)
      {
            try
            {
                  var Staff = await _services.AddStaff(addStaff, cancellationToken);
                  return Ok(Staff);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Add Staff " + e);
            }
      }
}
