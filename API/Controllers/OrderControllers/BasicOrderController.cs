using API.Services.OrderServices.InterFaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderControllers;

[ApiController]
[Route("api/order")]
[Authorize]
public partial class OrderController(
    IOrderServices orderService,
    ILogger<OrderController> logger) : ControllerBase
{
    private readonly IOrderServices _orderService = orderService;
    private readonly ILogger<OrderController> _logger = logger;
}
