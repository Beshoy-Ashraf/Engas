using API.Contract.Item;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ItemControllers;

public partial class ItemController : ControllerBase
{
      [HttpDelete("{id:Guid}")]
      public async Task<ActionResult<ItemResponse>> DeleteItem([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  await _itemService.DeleteItem(id, cancellationToken);
                  return Ok();
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Delete Item " + e);
            }
      }


}

