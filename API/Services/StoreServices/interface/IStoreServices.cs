using API.Contract.Store;

namespace API.Services.StoreServices.interfaces
{
      public interface IStoreServices
      {
            Task<List<GetStore>> GetStores(CancellationToken cancellationToken);
            Task<GetStore> GetStore(Guid id, CancellationToken cancellationToken);
            Task<Guid> UpdateStorePassword(Guid id, String password, CancellationToken cancellationToken);
            Task<Guid> UpdateStore(Guid id, AddStore storeData, CancellationToken cancellationToken);
            Task DeleteStore(Guid id, CancellationToken cancellationToken);
      }
}
