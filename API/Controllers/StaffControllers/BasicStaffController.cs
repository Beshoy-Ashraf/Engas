using Microsoft.AspNetCore.Mvc;
using API.Services.StaffServices.interfaces;

[Route("api/Staff")]
[ApiController]
public partial class StaffController(ILogger<StaffController> logger, IStaffServices services) : ControllerBase
{
      private readonly ILogger<StaffController> _logger = logger;
      public IStaffServices _services { get; } = services;
}
