
using API.Contract.StoreStock;
using API.Services.StoreStockService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StoreStockControllers;


public partial class StoreStockController : ControllerBase
{
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateStoreStock([FromRoute] Guid id, [FromBody] StoreStockRequest updatedStoreStock, CancellationToken cancellationToken)
    {
        try
        {
            var updatedStoreStockId = await _StoreStockService.UpdateStoreStockItem(id, updatedStoreStock, cancellationToken);
            return Ok(new { Id = updatedStoreStockId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating store stock with ID {id}.");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id:guid}/quantity")]
    [Authorize]
    public async Task<IActionResult> UpdateStoreStockQuantity([FromRoute] Guid id, [FromBody] int newQuantity, CancellationToken cancellationToken)
    {
        try
        {
            var updatedStoreStockId = await _StoreStockService.UpdateStockItemQuantity(id, newQuantity, cancellationToken);
            return Ok(new { Id = updatedStoreStockId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating quantity for store stock with ID {id}.");
            return StatusCode(500, ex.Message);
        }
    }

}

