using API.Contract.Store;
using API.Data.Models.Store;
using Microsoft.AspNetCore.Mvc;

public partial class StoreController : ControllerBase
{

      [HttpPut("{id:Guid}")]
      public async Task<ActionResult<Guid>> UpdateStore([FromRoute] Guid id, [FromBody] AddStore addStore, CancellationToken cancellationToken)
      {
            try
            {
                  var store = await _services.UpdateStore(id, addStore, cancellationToken);
                  return Ok(store);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Update Store " + e);
            }
      }
      [HttpPut("password")]
      public async Task<ActionResult<Guid>> UpdateStorePassword([FromRoute] Guid id, [FromRoute] string password, CancellationToken cancellationToken)
      {
            try
            {
                  var store = await _services.UpdateStorePassword(id, password, cancellationToken);
                  return Ok(store);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Update Store  Password" + e);
            }
      }

}
