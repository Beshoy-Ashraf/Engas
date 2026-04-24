using API.Services.StoreServices.interfaces;
using Microsoft.AspNetCore.Mvc;


[Route("api/store")]
[ApiController]
public partial class StoreController(ILogger<StoreController> logger, IStoreServices services) : ControllerBase
{
      private readonly ILogger<StoreController> _logger = logger;
      public IStoreServices _services { get; } = services;
}
