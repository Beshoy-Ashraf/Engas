namespace API.Contract.Staff;

public class AddStaff
{
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public required string UserName { get; set; }
      public required string Password { get; set; }

}
