using API.Contract.Order;
using API.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderControllers;

public partial class OrderController : ControllerBase
{
      [HttpPut("{id:guid}")]
      public async Task<ActionResult<Guid>> UpdateOrder([FromRoute] Guid id, [FromBody] OrderRequest request, CancellationToken cancellationToken)
      {
            try
            {
                  var updatedId = await _orderService.UpdateOrder(id, request, cancellationToken);
                  return Ok(updatedId);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error updating order.");
                  return BadRequest("Can't Update Order " + e.Message);
            }
      }

      [HttpPut("{orderId:guid}/items/{orderItemId:guid}")]
      public async Task<IActionResult> UpdateOrderItems(
            [FromRoute] Guid orderId,
            [FromRoute] Guid orderItemId,
            [FromBody] List<OrderItemsRequest> request,
            CancellationToken cancellationToken)
      {
            try
            {
                  await _orderService.UpdateOrderItems(orderId, orderItemId, request, cancellationToken);
                  return Ok("Order items updated successfully.");
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error updating order items.");
                  return BadRequest("Can't Update Order Items " + e.Message);
            }
      }
      [HttpPut("{orderId:guid}/status")]
      public async Task<IActionResult> UpdateOrderStatus(
            [FromRoute] Guid orderId,
            [FromQuery] OrderStatus newStatus,
            CancellationToken cancellationToken)
      {
            try
            {
                  await _orderService.UpdateOrderStatus(orderId, newStatus, cancellationToken);
                  return Ok("Order status updated successfully.");
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error updating order status.");
                  return BadRequest("Can't Update Order Status " + e.Message);
            }
      }


}
