using API.Contract.Store;
using API.Data.Models.Store;
using Microsoft.AspNetCore.Mvc;

public partial class StoreController : ControllerBase
{

      [HttpGet("{id:Guid}")]
      public async Task<ActionResult<GetStore>> GetStore([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  var store = await _services.GetStore(id, cancellationToken);
                  return Ok(store);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Store " + e);
            }
      }
      [HttpGet]
      public async Task<ActionResult<List<GetStore>>> GetStores(CancellationToken cancellationToken)
      {
            try
            {
                  var store = await _services.GetStores(cancellationToken);
                  return Ok(store);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Stores " + e);
            }
      }
}
