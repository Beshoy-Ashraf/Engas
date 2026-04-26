using API.Services.ItemServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ItemControllers;

[ApiController]
[Route("api/item")]
[Authorize]
public partial class ItemController(
    IItemInterface itemService,
    ILogger<ItemController> logger) : ControllerBase
{
    private readonly IItemInterface _itemService = itemService;
    private readonly ILogger<ItemController> _logger = logger;

}

