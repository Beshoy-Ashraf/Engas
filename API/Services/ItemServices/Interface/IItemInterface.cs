using API.Contract.Item;


namespace API.Services.ItemServices.Interface;

public interface IItemInterface
{
      Task<List<ItemResponse>> GetItems(CancellationToken cancellationToken);
      Task<ItemResponse> GetItem(Guid id, CancellationToken cancellationToken);
      Task<Guid> UpdateItemPrice(Guid id, Double NewPrice, CancellationToken cancellationToken);
      Task<Guid> UpdateItem(Guid id, ItemRequest ItemData, CancellationToken cancellationToken);
      Task<Guid> AddItem(ItemRequest NewItem, CancellationToken cancellationToken);
      Task DeleteItem(Guid id, CancellationToken cancellationToken);
}
