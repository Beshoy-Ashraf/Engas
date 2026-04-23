using API.Contract.Staff;
using API.Data.Models.Staff;
using Microsoft.AspNetCore.Mvc;

public partial class StaffController : ControllerBase
{

      [HttpGet("{id:Guid}")]
      public async Task<ActionResult<GetStaff>> GetStaff([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  var Staff = await _services.GetStaff(id, cancellationToken);
                  return Ok(Staff);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Staff " + e);
            }
      }
      [HttpGet]
      public async Task<ActionResult<List<GetStaff>>> GetStaffs(CancellationToken cancellationToken)
      {
            try
            {
                  var Staff = await _services.GetStaffs(cancellationToken);
                  return Ok(Staff);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Staffs " + e);
            }
      }
}
