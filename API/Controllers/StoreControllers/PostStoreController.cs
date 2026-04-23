using API.Contract.Store;
using Microsoft.AspNetCore.Mvc;

public partial class StoreController : ControllerBase
{

      [HttpPost]
      public async Task<ActionResult<Guid>> AddStore([FromBody] AddStore addStore, CancellationToken cancellationToken)
      {
            try
            {
                  var store = await _services.AddStore(addStore, cancellationToken);
                  return Ok(store);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Add Store " + e);
            }
      }
}
