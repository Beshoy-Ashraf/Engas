using API.Contract.Item;
using API.Core;
using API.Data.Models.Item;
using API.Data.Models.StoreStock;
using API.Services.ItemServices.Interface;
using Microsoft.EntityFrameworkCore;


namespace API.Services.ItemServices;

public class ItemServices(AppDBContext dbContext, ILogger<ItemServices> logger) : IItemInterface
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly ILogger<ItemServices> _logger = logger;

      public async Task<Guid> UpdateItem(Guid id, ItemRequest ItemData, CancellationToken cancellationToken)
      {
            var Item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            Item.Model = ItemData.Model;
            Item.Brand = ItemData.Brand;
            Item.Description = ItemData.Description;
            Item.OldPrice = ItemData.OldPrice;
            Item.CurrentPrice = ItemData.CurrentPrice;
            Item.UpdatedDate = DateTime.UtcNow;
            _dbContext.Items.Update(Item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Item.Id;
      }
      public async Task<Guid> AddItem(ItemRequest NewItem, CancellationToken cancellationToken)
      {
            var storeStock = new List<StoreStockLevel>();

            var item = new Item
            {
                  Model = NewItem.Model,
                  Brand = NewItem.Brand,
                  Description = NewItem.Description,
                  OldPrice = NewItem.OldPrice,
                  CurrentPrice = NewItem.CurrentPrice,
                  StoreStocks = storeStock,
                  UpdatedDate = DateTime.UtcNow
            };
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
      }


      public async Task<Guid> UpdateItemPrice(Guid id, Double NewPrice, CancellationToken cancellationToken)
      {
            var Item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            Item.OldPrice = Item.CurrentPrice;
            Item.CurrentPrice = NewPrice;
            Item.UpdatedDate = DateTime.UtcNow;
            _dbContext.Items.Update(Item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Item.Id;
      }

      public async Task<ItemResponse> GetItem(Guid id, CancellationToken cancellationToken)
      {
            var Item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            var ItemData = new ItemResponse
            {
                  Id = Item.Id,
                  Brand = Item.Brand,
                  Model = Item.Model,
                  Description = Item.Description,
                  OldPrice = Item.OldPrice,
                  CurrentPrice = Item.CurrentPrice,
                  StockLocation = Item.StockLocation
            };
            return ItemData;
      }

      public async Task<List<ItemResponse>> GetItems(CancellationToken cancellationToken)
      {
            var Items = await _dbContext.Items
                  .Where(x => x.DeletedDate == null)
                  .ToListAsync(cancellationToken);

            var ItemData = Items.Select(Item => new ItemResponse
            {
                  Id = Item.Id,
                  Brand = Item.Brand,
                  Model = Item.Model,
                  Description = Item.Description,
                  OldPrice = Item.OldPrice,
                  CurrentPrice = Item.CurrentPrice,
                  StockLocation = Item.StockLocation


            }).ToList();

            return ItemData;
      }

      public async Task DeleteItem(Guid id, CancellationToken cancellationToken)
      {
            var Item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            Item.DeletedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
      }

}
