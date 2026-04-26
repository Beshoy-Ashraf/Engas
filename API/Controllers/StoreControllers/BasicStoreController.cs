using API.Services.StoreServices.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/store")]
[ApiController]
[Authorize(Roles = "Admin")]

public partial class StoreController(ILogger<StoreController> logger, IStoreServices services) : ControllerBase
{
      private readonly ILogger<StoreController> _logger = logger;
      public IStoreServices _services { get; } = services;
}
