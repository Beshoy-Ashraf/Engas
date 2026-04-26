namespace API.Contract.Staff;

public class AddStaff
{
      public required string FirstName { get; set; } = "";
      public required string LastName { get; set; } = "";
      public string Phone { get; set; } = "";
      public required string UserName { get; set; }
      public required string Password { get; set; }
      public string SSN { get; set; } = "";
}
