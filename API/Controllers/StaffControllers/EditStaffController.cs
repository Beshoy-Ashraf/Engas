using API.Contract.Staff;
using API.Data.Models.Staff;
using Microsoft.AspNetCore.Mvc;

public partial class StaffController : ControllerBase
{

      [HttpPut("{id:Guid}")]
      public async Task<ActionResult<Guid>> UpdateStaff([FromRoute] Guid id, [FromBody] AddStaff addStaff, CancellationToken cancellationToken)
      {
            try
            {
                  var Staff = await _services.UpdateStaff(id, addStaff, cancellationToken);
                  return Ok(Staff);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Update Staff " + e);
            }
      }
      [HttpPut("password")]
      public async Task<ActionResult<Guid>> UpdateStaffPassword([FromRoute] Guid id, [FromRoute] string password, CancellationToken cancellationToken)
      {
            try
            {
                  var Staff = await _services.UpdateStaffPassword(id, password, cancellationToken);
                  return Ok(Staff);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Update Staff  Password" + e);
            }
      }

}
