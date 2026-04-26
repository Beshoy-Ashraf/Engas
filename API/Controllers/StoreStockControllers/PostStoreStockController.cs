
using API.Contract.StoreStock;
using API.Services.StoreStockService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StoreStockControllers;


public partial class StoreStockController : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddStoreStock([FromBody] StoreStockRequest newStoreStock, CancellationToken cancellationToken)
    {
        try
        {
            var newStoreStockId = await _StoreStockService.AddStoreStock(newStoreStock, cancellationToken);
            return CreatedAtAction(nameof(_StoreStockService.GetStoreStock), new { id = newStoreStockId }, new { Id = newStoreStockId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding new store stock.");
            return StatusCode(500, ex.Message);
        }
    }

}

