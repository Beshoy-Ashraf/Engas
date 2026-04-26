using System.ComponentModel.DataAnnotations;

namespace API.Contract.User.Request;

public class TokenRequest
{
      [Required]
      public string Token { get; set; } = null!;

      [Required]
      public string RefreshToken { get; set; } = null!;
}
