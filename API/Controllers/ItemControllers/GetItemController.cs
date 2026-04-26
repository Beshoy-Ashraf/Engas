using API.Contract.Item;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ItemControllers;

public partial class ItemController : ControllerBase
{
      [HttpGet("{id:Guid}")]
      public async Task<ActionResult<ItemResponse>> GetItem([FromRoute] Guid id, CancellationToken cancellationToken)
      {
            try
            {
                  var Item = await _itemService.GetItem(id, cancellationToken);
                  return Ok(Item);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Item " + e);
            }
      }
      [HttpGet]
      public async Task<ActionResult<List<ItemResponse>>> GetItems(CancellationToken cancellationToken)
      {
            try
            {
                  var Item = await _itemService.GetItems(cancellationToken);
                  return Ok(Item);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't Get Items " + e);
            }
      }

}

