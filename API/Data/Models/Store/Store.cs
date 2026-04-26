namespace API.Data.Models.Store;

public class Store
{
      public Guid Id { get; set; }
      public string Name { get; set; } = "";
      public string Phone { get; set; } = "";
      public string City { get; set; } = "";
      public required string Code { get; set; }
      public required string Password { get; set; }
      public List<StoreStock.StoreStock> StoreStocks { get; set; } = [];

      public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
      public DateTime? DeletedDate { get; set; }


}
