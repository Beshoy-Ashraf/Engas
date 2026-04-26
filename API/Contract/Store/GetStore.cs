namespace API.Contract.Store;

public class GetStore
{
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public string City { get; set; } = "";
      public required string Code { get; set; }
}
