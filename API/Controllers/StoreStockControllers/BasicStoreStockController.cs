
using API.Services.StoreStockService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StoreStockControllers;

[ApiController]
[Route("api/store-stock")]
[Authorize]
public partial class StoreStockController(
    IStoreStockService StoreStockService,
    ILogger<StoreStockController> logger) : ControllerBase
{
    private readonly IStoreStockService _StoreStockService = StoreStockService;
    private readonly ILogger<StoreStockController> _logger = logger;

}

