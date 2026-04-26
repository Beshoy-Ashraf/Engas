using API.Contract.StoreStock;

namespace API.Services.StoreStockService.Interface;

public interface IStoreStockService
{

      Task<List<StoreStockResponse>> GetStoreStocks(CancellationToken cancellationToken);
      Task<List<ItemsInStores>> GetItemStores(Guid id, CancellationToken cancellationToken);
      Task<StoreStockResponse> GetStoreStock(Guid id, CancellationToken cancellationToken);
      Task<Guid> UpdateStockItemQuantity(Guid id, int Quantity, CancellationToken cancellationToken);
      Task<Guid> UpdateStoreStockItem(Guid id, StoreStockRequest StoreStockData, CancellationToken cancellationToken);
      Task<Guid> AddStoreStock(StoreStockRequest NewStoreStock, CancellationToken cancellationToken);
      Task DeleteStoreStock(Guid id, CancellationToken cancellationToken);
}
