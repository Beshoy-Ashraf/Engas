using Microsoft.AspNetCore.Mvc;
using API.Services.StaffServices.interfaces;

[Route("api/Staff")]
[ApiController]
public partial class StaffController : ControllerBase
{
      private readonly ILogger<StaffController> _logger;
      public IStaffServices _services { get; }

      public StaffController(ILogger<StaffController> logger, IStaffServices services)
      {
            _logger = logger;
            _services = services;
      }

}
