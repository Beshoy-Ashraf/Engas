using API.Contract.Order;
using API.Core.Enums;

namespace API.Services.OrderServices.InterFaces;

public interface IOrderServices
{

      public Task<Guid> CreateOrder(OrderRequest orderRequest, CancellationToken cancellationToken);
      public Task<Guid> UpdateOrder(Guid orderId, OrderRequest orderRequest, CancellationToken cancellationToken);
      public Task DeleteOrder(Guid orderId);
      public Task<OrderResponse> GetOrderById(Guid orderId);
      public Task<List<OrderResponse>> GetAllOrders();
      public Task UpdateOrderItems(Guid orderId, Guid orderItemID, List<OrderItemsRequest> orderItemsRequest, CancellationToken cancellationToken);
      public Task DeleteOrderItems(Guid orderId, Guid OrderItemId, CancellationToken cancellationToken);
      public Task<List<OrderResponse>> GetOrdersByCustomerId(Guid customerId);
      public Task<List<OrderResponse>> GetOrdersByStatus(OrderStatus status);
      public Task<List<OrderResponse>> GetOrdersByPaymentMethod(string paymentMethod);
      public Task<List<OrderResponse>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
      public Task<List<OrderResponse>> GetOrdersByTotalAmountRange(double minTotalAmount, double maxTotalAmount);
      public Task<List<OrderResponse>> GetOrdersByItemId(Guid itemId);
      public Task<List<OrderResponse>> GetOrdersByItemSerial(string itemSerial);
      public Task<List<OrderResponse>> GetOrdersByBrand(string brand);
      public Task<List<OrderResponse>> GetOrdersByModel(string model);
      public Task<List<OrderResponse>> GetOrdersByStoreCode(string storeCode);
      public Task<List<OrderResponse>> GetStoreOrders(Guid storeId);
      public Task<List<OrderResponse>> GetCustomerOrders(Guid customerId);
      public Task<double> GetStoreTotalOrdersAmount(Guid storeId);
      public Task<double> GetStoreTotalOrdersAmountByBate(Guid storeId, DateTime startDate, DateTime endDate);
      public Task<double> GetCustomerTotalOrdersAmount(Guid customerId);
      public Task<double> GetCustomerTotalOrdersAmountByBate(Guid customerId, DateTime startDate, DateTime endDate);
      public Task<double> GetNumberOfItemGetFromOneStore(Guid StoreID, DateTime startDate, DateTime endDate);
      public Task<double> GetTotalStaffSellingAmount(Guid staffId, DateTime startDate, DateTime endDate);
      public Task UpdateOrderStatus(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken);
      public Task UpdateOrderItemPdf(Guid orderId, Guid orderItemId, byte[] pdfData, string pdfFileName, CancellationToken cancellationToken);
      public Task<(byte[] PdfData, string PdfFileName)> GetOrderItemPdf(Guid orderItemId, CancellationToken cancellationToken);








}
