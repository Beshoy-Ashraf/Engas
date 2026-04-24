namespace API.Contract.User.Request;

public class StoreLoginRequest
{
      public required string StoreCode { get; set; }
      public required string Password { get; set; }
}
