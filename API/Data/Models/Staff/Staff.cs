using API.Core.Enums;

namespace API.Data.Models.Staff;

public class Staff
{
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public required string UserName { get; set; }
      public required string Password { get; set; }
      public string SSN { get; set; } = "";
      public UserEnum Role { get; set; } = UserEnum.Staff;
      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }
}
