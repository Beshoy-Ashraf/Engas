namespace API.Contract.Staff;

public class GetStaff
{
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public required string UserName { get; set; }
      public required string SSN { get; set; }

}
