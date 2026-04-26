
using API.Contract.StoreStock;
using API.Services.StoreStockService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StoreStockControllers;


public partial class StoreStockController : ControllerBase
{
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteStoreStock([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _StoreStockService.DeleteStoreStock(id, cancellationToken);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting store stock with ID {id}.");
            return StatusCode(500, ex.Message);
        }
    }

}

