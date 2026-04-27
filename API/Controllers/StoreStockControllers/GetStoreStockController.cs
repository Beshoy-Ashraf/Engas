using API.Contract.StoreStock;
using API.Services.StoreStockService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StoreStockControllers;


public partial class StoreStockController : ControllerBase
{
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetStoreStock([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var storeStock = await _StoreStockService.GetStoreStock(id, cancellationToken);
            if (storeStock == null)
            {
                return NotFound($"Store stock with ID {id} not found.");
            }
            return Ok(storeStock);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving store stock with ID {id}.");
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetStoreStocks(CancellationToken cancellationToken)
    {
        try
        {
            var storeStocks = await _StoreStockService.GetStoreStocks(cancellationToken);
            return Ok(storeStocks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving store stocks.");
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet("item/{itemId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetItemInAllStores([FromRoute] Guid itemId, CancellationToken cancellationToken)
    {
        try
        {
            var storeStocks = await _StoreStockService.GetItemInStores(itemId, cancellationToken);
            return Ok(storeStocks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving store stocks for item ID {itemId}.");
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet("store/{storeId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetItemsInStore([FromRoute] Guid storeId,
        CancellationToken cancellationToken)
    {
        try
        {
            var itemsInStore = await _StoreStockService.GetStoreItems(storeId, cancellationToken);
            return Ok(itemsInStore);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving items in store with ID {storeId}.");
            return StatusCode(500, ex.Message);
        }
    }

}

