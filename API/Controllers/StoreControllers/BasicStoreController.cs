using API.Services.StoreServices.interfaces;
using Microsoft.AspNetCore.Mvc;


[Route("api/store")]
[ApiController]
public partial class StoreController : ControllerBase
{
      private readonly ILogger<StoreController> _logger;
      public IStoreServices _services { get; }

      public StoreController(ILogger<StoreController> logger, IStoreServices services)
      {
            _logger = logger;
            _services = services;
      }

}
