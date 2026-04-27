using API.Services.CustomerService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

[ApiController]
[Route("api/customer")]
[Authorize]
public partial class CustomerController(
    ICustomerService customerService,
    ILogger<CustomerController> logger) : ControllerBase
{
      private readonly ICustomerService _customerService = customerService;
      private readonly ILogger<CustomerController> _logger = logger;

}

