using API.Core.Enums;

namespace API.Contract.User.Request;

public class UserVerificationRequest
{
      public string? StaffId { get; set; }
      public UserEnum UserType { get; set; }
}
