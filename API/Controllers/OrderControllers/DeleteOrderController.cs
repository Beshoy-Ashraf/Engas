using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderControllers;

public partial class OrderController : ControllerBase
{
      [HttpDelete("{id:guid}")]
      public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
      {
            try
            {
                  await _orderService.DeleteOrder(id);
                  return Ok("Order deleted successfully.");
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error deleting order.");
                  return BadRequest("Can't Delete Order " + e.Message);
            }
      }

      [HttpDelete("{orderId:guid}/items/{orderItemId:guid}")]
      public async Task<IActionResult> DeleteOrderItems(
            [FromRoute] Guid orderId,
            [FromRoute] Guid orderItemId,
            CancellationToken cancellationToken)
      {
            try
            {
                  await _orderService.DeleteOrderItems(orderId, orderItemId, cancellationToken);
                  return Ok("Order item deleted successfully.");
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error deleting order item.");
                  return BadRequest("Can't Delete Order Item " + e.Message);
            }
      }
}
