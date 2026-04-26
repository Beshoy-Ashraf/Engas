using API.Contract.Item;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ItemControllers;

public partial class ItemController : ControllerBase
{
      [HttpPut("{id:Guid}")]
      public async Task<ActionResult<ItemResponse>> UpdateItem([FromRoute] Guid id, ItemRequest itemRequest, CancellationToken cancellationToken)
      {
            try
            {
                  var Item = await _itemService.UpdateItem(id, itemRequest, cancellationToken);
                  return Ok(Item);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't update Item " + e);
            }
      }
      [HttpPut("{id:Guid}/price/{price:double}")]
      public async Task<ActionResult<List<ItemResponse>>> UpdateItemPrice([FromRoute] Guid id, [FromRoute] double price, CancellationToken cancellationToken)
      {
            try
            {
                  var Item = await _itemService.UpdateItemPrice(id, price, cancellationToken);
                  return Ok(Item);
            }
            catch (Exception e)
            {
                  return BadRequest("Can't update Item price " + e);
            }
      }

}

