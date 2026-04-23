namespace API.Contract.Store;

public class AddStore
{
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public string City { get; set; } = "";
      public required string Code { get; set; }
      public required string Password { get; set; }
}
