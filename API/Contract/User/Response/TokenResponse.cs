namespace API.Contract.User.Response;

public class TokenResponse
{
      public string Token { get; set; } = null!;
      public string RefreshToken { get; set; } = null!;
      public string UserId { get; set; } = null!;
}
