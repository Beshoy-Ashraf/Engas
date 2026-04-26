using Microsoft.AspNetCore.Mvc;
using API.Services.StaffServices.interfaces;
using Microsoft.AspNetCore.Authorization;

[Route("api/Staff")]
[ApiController]
[Authorize(Roles = "Admin")]
public partial class StaffController(ILogger<StaffController> logger, IStaffServices services) : ControllerBase
{
      private readonly ILogger<StaffController> _logger = logger;
      public IStaffServices _services { get; } = services;
}
