using API.Contract.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ItemControllers;

public partial class ItemController : ControllerBase
{
      [HttpPost]
      [Authorize]
      public async Task<IActionResult> AddItem([FromBody] ItemRequest newItem, CancellationToken cancellationToken)
      {
            try
            {
                  var newItemId = await _itemService.AddItem(newItem, cancellationToken);
                  return CreatedAtAction(nameof(_itemService.GetItem), new { id = newItemId }, new { Id = newItemId });
            }
            catch (ArgumentException ex)
            {
                  return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                  _logger.LogError(ex, "Error adding new item.");
                  return StatusCode(500, ex.Message);
            }
      }
}

