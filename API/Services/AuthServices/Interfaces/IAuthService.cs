
using API.Contract.User.Request;
using API.Contract.User.Response;

namespace API.Services.AuthServices;

public interface IAuthService
{
    Task<TokenResponse> StaffLoginAsync(UserLoginRequest loginRequest, CancellationToken ct);
    Task<TokenResponse> StaffRegisterAsync(UserRegistrationRequest registrationRequest, CancellationToken ct);
    Task<TokenResponse> StoreRegisterAsync(StoreRegistrationRequest registerRequest, CancellationToken ct);
    Task<TokenResponse> StoreLoginAsync(StoreLoginRequest loginRequest, CancellationToken ct);
}
