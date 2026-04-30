using API.Contract.Order;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderControllers;

public partial class OrderController : ControllerBase
{
      [HttpPost]
      public async Task<IActionResult> PostOrder([FromBody] OrderRequest order, CancellationToken cancellationToken)
      {
            var result = await _orderService.CreateOrder(order, cancellationToken);
            if (result != Guid.Empty)
            {
                  return Ok("Order created successfully.");
            }
            else
            {
                  return BadRequest("Failed to create order.");
            }
      }
      [HttpPut("{orderId:guid}/items/{orderItemId:guid}/pdf")]
      public async Task<IActionResult> UpdateOrderItemPdf(
           [FromRoute] Guid orderId,
           [FromRoute] Guid orderItemId,
           [FromForm] IFormFile pdfFile,

           CancellationToken cancellationToken)
      {
            try
            {
                  if (pdfFile == null || pdfFile.Length == 0)
                        return BadRequest("No PDF file uploaded.");
                  byte[] pdfData;
                  using (var memoryStream = new MemoryStream())
                  {
                        await pdfFile.CopyToAsync(memoryStream, cancellationToken);
                        pdfData = memoryStream.ToArray();
                  }
                  await _orderService.UpdateOrderItemPdf(orderId, orderItemId, pdfData, pdfFile.FileName, cancellationToken);

                  return Ok("PDF file updated successfully.");




            }
            catch (Exception ex)
            {
                  // Log the exception (not implemented here)
                  return StatusCode(500, $"Internal server error: {ex.Message}");
            }

      }
}
