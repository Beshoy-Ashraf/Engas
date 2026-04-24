using API.Core.Enums;

namespace API.Contract.User.Request;

public class UserRegistrationRequest
{
      public UserEnum UserType { get; set; }
      public required string FirstName { get; set; }
      public required string LastName { get; set; }
      public required string UserName { get; set; }
      public required string Password { get; set; }
      public required string Phone { get; set; }

      public required string SSN { get; set; }
}
