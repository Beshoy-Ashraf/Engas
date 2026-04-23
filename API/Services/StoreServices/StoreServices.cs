using API.Contract.Store;
using API.Data.Models.Store;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services.StoreServices;

public class StoreServices(AppDBContext dbContext, IServiceProvider serviceProvider, ILogger<StoreServices> logger)
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly IServiceProvider _serviceProvider = serviceProvider;
      private readonly ILogger<StoreServices> _logger = logger;

      public async Task<Guid> AddStore(AddStore newStore, CancellationToken cancellationToken)
      {
            var store = new Store
            {
                  Name = newStore.Name,
                  Password = newStore.Password,
                  Code = newStore.Code,
                  City = newStore.City,
                  Phone = newStore.Phone,
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow,
            };
            await _dbContext.Stores.AddAsync(store, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return store.Id;

      }
      public async Task<Guid> UpdateStore(Guid id, AddStore storeData, CancellationToken cancellationToken)
      {

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
            store.Name = storeData.Name;
            store.Password = storeData.Password;
            store.Code = storeData.Code;
            store.City = storeData.City;
            store.Phone = storeData.Phone;
            store.UpdatedDate = DateTime.UtcNow;
            _dbContext.Stores.Update(store);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return store.Id;

      }
      public async Task<Guid> UpdateStorePassword(Guid id, String password, CancellationToken cancellationToken)
      {

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
            store.Password = password;
            store.UpdatedDate = DateTime.UtcNow;
            _dbContext.Stores.Update(store);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return store.Id;

      }
      public async Task<GetStore> GetStore(Guid id, CancellationToken cancellationToken)
      {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
            var storeData = new GetStore
            {
                  Id = store.Id,
                  Name = store.Name,
                  Code = store.Code,
                  City = store.City,
                  Phone = store.Phone,
            };
            return storeData;
      }
      public async Task<List<GetStore>> GetStores(CancellationToken cancellationToken)
      {
            var stores = await _dbContext.Stores
                  .Where(x => x.DeletedDate == default(DateTime))
                  .ToListAsync(cancellationToken);

            var storeData = stores.Select(store => new GetStore
            {
                  Id = store.Id,
                  Name = store.Name,
                  Code = store.Code,
                  City = store.City,
                  Phone = store.Phone,
            }).ToList();

            return storeData;
      }
      public async Task DeleteStore(Guid id, CancellationToken cancellationToken)
      {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == default(DateTime), cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
            store.DeletedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
      }
}
