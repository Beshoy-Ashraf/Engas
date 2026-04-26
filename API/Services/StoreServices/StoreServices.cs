using API.Contract.Store;
using API.Data.Models.Store;
using API.Services.StoreServices.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services.StoreServices;

public class StoreServices(AppDBContext dbContext, ILogger<StoreServices> logger) : IStoreServices
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly ILogger<StoreServices> _logger = logger;


      public async Task<Guid> UpdateStore(Guid id, AddStore storeData, CancellationToken cancellationToken)
      {

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
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

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
            store.Password = password;
            store.UpdatedDate = DateTime.UtcNow;
            _dbContext.Stores.Update(store);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return store.Id;

      }
      public async Task<GetStore> GetStore(Guid id, CancellationToken cancellationToken)
      {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
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
                  .Where(x => x.DeletedDate == null)
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
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == id && x.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Store with ID {id} was not found.");
            store.DeletedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
      }


}
