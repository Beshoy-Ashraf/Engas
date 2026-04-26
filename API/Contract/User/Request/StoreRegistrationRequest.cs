namespace API.Contract.User.Request;

public class StoreRegistrationRequest
{
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public string City { get; set; } = "";
      public required string Code { get; set; }
      public required string Password { get; set; }
}
