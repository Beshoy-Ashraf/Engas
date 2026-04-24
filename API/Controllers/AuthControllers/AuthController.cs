using API.Contract.User.Request;
using API.Contract.User.Response;
using API.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
      private readonly IAuthService _authService;

      public AuthController(IAuthService authService)
      {
            _authService = authService;
      }

      [HttpPost("register")]
      public async Task<ActionResult<TokenResponse>> Register([FromBody] UserRegistrationRequest request, CancellationToken ct)
      {
            try
            {
                  var response = await _authService.StaffRegisterAsync(request, ct);
                  return Ok(response);
            }
            catch (ArgumentException ex)
            {
                  return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                  return StatusCode(500, ex.Message);
            }
      }

      [HttpPost("login")]
      public async Task<ActionResult<TokenResponse>> Login([FromBody] UserLoginRequest request, CancellationToken ct)
      {
            try
            {
                  var response = await _authService.StaffLoginAsync(request, ct);
                  return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                  return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                  return StatusCode(500, ex.Message);
            }
      }
      [HttpPost("store/register")]
      public async Task<ActionResult<TokenResponse>> StoreRegister([FromBody] StoreRegistrationRequest request, CancellationToken ct)
      {
            try
            {
                  var response = await _authService.StoreRegisterAsync(request, ct);
                  return Ok(response);
            }
            catch (ArgumentException ex)
            {
                  return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                  return StatusCode(500, ex.Message);
            }
      }

      [HttpPost("store/login")]
      public async Task<ActionResult<TokenResponse>> StoreLogin([FromBody] StoreLoginRequest request, CancellationToken ct)
      {
            try
            {
                  var response = await _authService.StoreLoginAsync(request, ct);
                  return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                  return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                  return StatusCode(500, ex.Message);
            }
      }
}

