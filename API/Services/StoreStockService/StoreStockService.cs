using API.Contract.StoreStock;
using API.Data.Models.StoreStock;
using API.Services.StoreStockService.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Services.StoreStockService;

public class StoreStockService(AppDBContext dbContext, ILogger<StoreStockService> logger) : IStoreStockService
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly ILogger<IStoreStockService> _logger = logger;

      public async Task<Guid> AddStoreStock(StoreStockRequest NewStoreStock, CancellationToken cancellationToken)
      {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == NewStoreStock.StoreId && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {NewStoreStock.StoreId} was not found.");
            var item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == NewStoreStock.ItemId && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {NewStoreStock.ItemId} was not found.");
            var StoreStock = new StoreStockLevel
            {
                  StoreId = NewStoreStock.StoreId,
                  ItemId = NewStoreStock.ItemId,
                  Quantity = NewStoreStock.Quantity,
                  Store = store,
                  Item = item,
                  UpdatedDate = DateTime.UtcNow
            };
            _dbContext.StoreStockLevels.Add(StoreStock);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StoreStock.Id;
      }
      public async Task<Guid> UpdateStockItemQuantity(Guid id, int Quantity, CancellationToken cancellationToken)
      {
            var StoreStock = await _dbContext.StoreStockLevels.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            StoreStock.Quantity = Quantity;
            StoreStock.UpdatedDate = DateTime.UtcNow;
            _dbContext.StoreStockLevels.Update(StoreStock);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StoreStock.Id;
      }
      public async Task<Guid> UpdateStoreStockItem(Guid id, StoreStockRequest storeStockRequest, CancellationToken cancellationToken)
      {
            var StoreStock = await _dbContext.StoreStockLevels.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == storeStockRequest.StoreId && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {storeStockRequest.StoreId} was not found.");
            var item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == storeStockRequest.ItemId && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {storeStockRequest.ItemId} was not found.");
            StoreStock.Quantity = storeStockRequest.Quantity;
            StoreStock.ItemId = storeStockRequest.ItemId;
            StoreStock.StoreId = storeStockRequest.StoreId;
            StoreStock.Store = store;
            StoreStock.Item = item;
            StoreStock.UpdatedDate = DateTime.UtcNow;
            _dbContext.StoreStockLevels.Update(StoreStock);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return StoreStock.Id;
      }
      public async Task DeleteStoreStock(Guid id, CancellationToken cancellationToken)
      {
            var StoreStock = await _dbContext.StoreStockLevels.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            StoreStock.DeletedDate = DateTime.UtcNow;
            _dbContext.StoreStockLevels.Update(StoreStock);
            await _dbContext.SaveChangesAsync(cancellationToken);
      }

      public async Task<StoreStockResponse> GetStoreStock(Guid id, CancellationToken cancellationToken)
      {
            var StoreStock = await _dbContext.StoreStockLevels.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Item with ID {id} was not found.");
            return new StoreStockResponse
            {
                  Id = StoreStock.Id,
                  StoreId = StoreStock.StoreId,
                  ItemId = StoreStock.ItemId,
                  Quantity = StoreStock.Quantity
            };
      }


      public async Task<List<StoreStockResponse>> GetStoreStocks(CancellationToken cancellationToken)
      {
            var StoreStocks = await _dbContext.StoreStockLevels
                  .Include(x => x.Item)
                  .Where(x => x.DeletedDate == null)
                  .ToListAsync(cancellationToken: cancellationToken);
            return [..StoreStocks.Select(StoreStock => new StoreStockResponse
            {
                  Id = StoreStock.Id,
                  StoreId = StoreStock.StoreId,
                  ItemId = StoreStock.ItemId,
                  Quantity = StoreStock.Quantity,
                  ItemCurrentPrice = StoreStock.Item.CurrentPrice

            })];
      }

      public async Task<List<ItemsInStores>> GetItemInStores(Guid id, CancellationToken cancellationToken)
      {
            var StoreStocks = await _dbContext.StoreStockLevels
            .Include(x => x.Item).
            Include(x => x.Store).
            Where(x => x.ItemId == id && x.DeletedDate == null).ToListAsync(cancellationToken: cancellationToken);
            return [.. StoreStocks.Select(StoreStock => new ItemsInStores
            {
                  Id = StoreStock.Id,
                  StoreId = StoreStock.StoreId,
                  ItemId = StoreStock.ItemId,
                  Quantity = StoreStock.Quantity,
                  StoreName = StoreStock.Store.Name,
                  ItemDescription = StoreStock.Item.Description,
                  ItemCurrentPrice = StoreStock.Item.CurrentPrice
            })];
      }
      public async Task<List<ItemsInStores>> GetStoreItems(Guid id, CancellationToken cancellationToken)
      {
            var StoreStocks = await _dbContext.StoreStockLevels
            .Include(x => x.Item).
            Include(x => x.Store).
            Where(x => x.StoreId == id && x.DeletedDate == null).ToListAsync(cancellationToken: cancellationToken);
            return [.. StoreStocks.Select(StoreStock => new ItemsInStores
            {
                  Id = StoreStock.Id,
                  StoreId = StoreStock.StoreId,
                  ItemId = StoreStock.ItemId,
                  Quantity = StoreStock.Quantity,
                  StoreName = StoreStock.Store.Name,
                  ItemDescription = StoreStock.Item.Description,
                  ItemCurrentPrice = StoreStock.Item.CurrentPrice
            })];
      }



}
