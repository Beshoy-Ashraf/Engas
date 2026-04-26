using API.Contract.Store;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public partial class StoreController : ControllerBase
{

      [HttpDelete]
      public async Task<ActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  await _services.DeleteStore(id, cancellationToken);
                  return Ok();
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Add Store " + e);
            }
      }
}
