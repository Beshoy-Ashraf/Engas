using API.Contract.Order;
using API.Core.Enums;
using API.Data.Models.Order;
using API.Services.OrderServices.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services.OrderServices;

public class OrderServices(AppDBContext dbContext, ILogger<OrderServices> logger) : IOrderServices
{
      private readonly AppDBContext _dbContext = dbContext;
      private readonly ILogger<OrderServices> _logger = logger;

      public Task<Guid> CreateOrder(OrderRequest orderRequest, CancellationToken cancellationToken)
      {
            var order = new Data.Models.Order.Order
            {
                  CustomerId = orderRequest.CustomerId,
                  TotalAmount = orderRequest.TotalAmount,
                  StaffId = orderRequest.StaffId,
                  PaymentMethod = orderRequest.PaymentMethod,
                  Status = orderRequest.Status,

                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow,
            };
            var orderItems = orderRequest.OrderItems.Select(oi => new Data.Models.Order.OrderItem
            {
                  ItemId = oi.ItemId,
                  ItemSerial = oi.ItemSerial,
                  IsDelivered = oi.IsDelivered,
                  PdfData = oi.PdfData,
                  PdfFileName = oi.PdfFileName,
            }).ToList();
            order.OrderItems = orderItems;

            _dbContext.Orders.Add(order);
            _dbContext.SaveChangesAsync(cancellationToken);
            return Task.FromResult(order.Id);
      }

      public Task<Guid> UpdateOrder(Guid orderId, OrderRequest orderRequest, CancellationToken cancellationToken)
      {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new Exception("Order not found");
            order.CustomerId = orderRequest.CustomerId;
            order.TotalAmount = orderRequest.TotalAmount;
            order.StaffId = orderRequest.StaffId;
            order.PaymentMethod = orderRequest.PaymentMethod;
            order.Status = orderRequest.Status;
            order.UpdatedDate = DateTime.UtcNow;
            var orderItems = orderRequest.OrderItems.Select(oi => new Data.Models.Order.OrderItem
            {
                  ItemId = oi.ItemId,
                  ItemSerial = oi.ItemSerial,
                  IsDelivered = oi.IsDelivered,
                  PdfData = oi.PdfData,
                  PdfFileName = oi.PdfFileName,

            }).ToList();
            order.OrderItems = orderItems;

            _dbContext.Orders.Update(order);
            _dbContext.SaveChangesAsync(cancellationToken);
            return Task.FromResult(order.Id);
      }

      public Task DeleteOrder(Guid orderId)
      {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new Exception("Order not found");
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChangesAsync();
            return Task.CompletedTask;
      }

      public Task<OrderResponse> GetOrderById(Guid orderId)
      {
            var order = _dbContext.Orders
                  .Include(o => o.OrderItems)
                  .ThenInclude(oi => oi.Item)
                  .Include(o => o.OrderItems)
                  .ThenInclude(oi => oi.Store)
                  .Include(x => x.Customer)

                  .FirstOrDefault(o => o.Id == orderId && o.DeletedDate == null) ?? throw new Exception("Order not found");
            var orderResponse = new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                   .Where(oi => oi.DeletedDate == null)
                  .Select(oi => new OrderItemResponse
                  {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        StoreId = oi.StoreId,
                        ItemId = oi.ItemId,
                        ItemSerial = oi.ItemSerial,
                        ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                        ItemModel = oi.Item != null ? oi.Item.Model : "",
                        ItemDescription = oi.Item != null ? oi.Item.Description : "",
                        IsDelivered = oi.IsDelivered,
                        ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                        StoreCity = oi.Store != null ? oi.Store.City : "",
                        StoreCode = oi.Store != null ? oi.Store.Code : "",
                        StoreName = oi.Store != null ? oi.Store.Name : "",
                        OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                        PdfFileName = oi.PdfFileName

                  }).ToList()
            };

            return Task.FromResult(orderResponse);
      }

      public Task<List<OrderResponse>> GetAllOrders()
      {
            var orders = _dbContext.Orders.Where(o => o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  TotalAmount = order.TotalAmount,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName

                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }

      public Task UpdateOrderItems(Guid orderId, Guid orderItemID, List<OrderItemsRequest> orderItemsRequest, CancellationToken cancellationToken)
      {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new Exception("Order not found");
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == orderItemID) ?? throw new Exception("Order item not found");
            var updatedOrderItems = orderItemsRequest.Select(oi => new Data.Models.Order.OrderItem
            {
                  ItemId = oi.ItemId,
                  ItemSerial = oi.ItemSerial,
                  IsDelivered = oi.IsDelivered,
                  PdfData = oi.PdfData,
                  PdfFileName = oi.PdfFileName,

            }).ToList();
            orderItem.ItemId = updatedOrderItems.First().ItemId;
            orderItem.ItemSerial = updatedOrderItems.First().ItemSerial;
            orderItem.IsDelivered = updatedOrderItems.First().IsDelivered;

            _dbContext.Orders.Update(order);
            _dbContext.SaveChangesAsync(cancellationToken);
            return Task.CompletedTask;
      }

      public Task DeleteOrderItems(Guid orderId, Guid OrderItemId, CancellationToken cancellationToken)
      {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new Exception("Order not found");
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == OrderItemId) ?? throw new Exception("Order item not found");
            orderItem.DeletedDate = DateTime.UtcNow;

            _dbContext.Orders.Update(order);
            _dbContext.SaveChangesAsync(cancellationToken);
            return Task.CompletedTask;
      }

      public Task<List<OrderResponse>> GetOrdersByCustomerId(Guid customerId)
      {
            var orders = _dbContext.Orders.Where(o => o.CustomerId == customerId && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  TotalAmount = order.TotalAmount,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }


      public Task<List<OrderResponse>> GetOrdersByStatus(OrderStatus status)
      {
            var orders = _dbContext.Orders.Where(o => o.Status == status && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  TotalAmount = order.TotalAmount,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName

                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByPaymentMethod(string paymentMethod)
      {
            var orders = _dbContext.Orders.Where(o => o.PaymentMethod == paymentMethod && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  TotalAmount = order.TotalAmount,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
      {
            var orders = _dbContext.Orders.Where(o => o.CreatedDate >= startDate && o.CreatedDate <= endDate && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByTotalAmountRange(double minTotalAmount, double maxTotalAmount)
      {
            var orders = _dbContext.Orders.Where(o => o.TotalAmount >= minTotalAmount && o.TotalAmount <= maxTotalAmount && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  TotalAmount = order.TotalAmount,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByItemId(Guid itemId)
      {
            var orders = _dbContext.Orders.Where(o => o.OrderItems.Any(oi => oi.ItemId == itemId) && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByItemSerial(string itemSerial)
      {
            var orders = _dbContext.Orders.Where(o => o.OrderItems.Any(oi => oi.ItemSerial == itemSerial) && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByBrand(string brand)
      {
            var orders = _dbContext.Orders.Where(o => o.OrderItems.Any(oi => oi.Item != null && oi.Item.Brand == brand) && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByModel(string model)
      {
            var orders = _dbContext.Orders.Where(o => o.OrderItems.Any(oi => oi.Item != null && oi.Item.Model == model) && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetOrdersByStoreCode(string storeCode)
      {
            var orders = _dbContext.Orders.Where(o => o.OrderItems.Any(oi => oi.Store != null && oi.Store.Code == storeCode) && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetStoreOrders(Guid storeId)
      {
            var orders = _dbContext.Orders.Where(o => o.OrderItems.Any(oi => oi.StoreId == storeId) && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,

                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();

            return Task.FromResult(orderResponses);
      }
      public Task<List<OrderResponse>> GetCustomerOrders(Guid customerId)
      {
            var orders = _dbContext.Orders.Where(o => o.CustomerId == customerId && o.DeletedDate == null).ToList();
            var orderResponses = orders.Select(order => new OrderResponse
            {
                  Id = order.Id,
                  CustomerId = order.CustomerId,
                  StaffId = order.StaffId,
                  StaffName = order.Staff != null ? order.Staff.Name : "",
                  TotalAmount = order.TotalAmount,
                  PaymentMethod = order.PaymentMethod,
                  Status = order.Status.ToString(),
                  CreatedDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  UpdatedDate = order.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                  OrderItems = order.OrderItems
                        .Where(oi => oi.DeletedDate == null)
                        .Select(oi => new OrderItemResponse
                        {
                              Id = oi.Id,
                              OrderId = oi.OrderId,
                              StoreId = oi.StoreId,
                              ItemId = oi.ItemId,
                              ItemSerial = oi.ItemSerial,
                              ItemBrand = oi.Item != null ? oi.Item.Brand : "",
                              ItemModel = oi.Item != null ? oi.Item.Model : "",
                              ItemDescription = oi.Item != null ? oi.Item.Description : "",
                              IsDelivered = oi.IsDelivered,
                              ItemPrice = oi.Item != null ? oi.Item.CurrentPrice : 0,
                              StoreCity = oi.Store != null ? oi.Store.City : "",
                              StoreCode = oi.Store != null ? oi.Store.Code : "",
                              StoreName = oi.Store != null ? oi.Store.Name : "",
                              OrderDate = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              CreatedDate = oi.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              UpdatedDate = oi.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                              HasPdf = oi.PdfData != null && oi.PdfData.Length > 0,
                              PdfFileName = oi.PdfFileName
                        }).ToList()
            }).ToList();
            return Task.FromResult(orderResponses);
      }
      public Task<double> GetStoreTotalOrdersAmount(Guid storeId)
      {
            var totalAmount = _dbContext.Orders
                  .Where(o => o.OrderItems.Any(oi => oi.StoreId == storeId) && o.DeletedDate == null)
                  .Sum(o => o.TotalAmount);
            return Task.FromResult(totalAmount);
      }
      public Task<double> GetStoreTotalOrdersAmountByBate(Guid storeId, DateTime startDate, DateTime endDate)
      {
            var totalAmount = _dbContext.Orders
                  .Where(o => o.OrderItems.Any(oi => oi.StoreId == storeId) && o.DeletedDate == null && o.CreatedDate >= startDate && o.CreatedDate <= endDate)
                  .Sum(o => o.TotalAmount);
            return Task.FromResult(totalAmount);
      }
      public Task<double> GetCustomerTotalOrdersAmount(Guid customerId)
      {
            var totalAmount = _dbContext.Orders
                  .Where(o => o.CustomerId == customerId && o.DeletedDate == null)
                  .Sum(o => o.TotalAmount);
            return Task.FromResult(totalAmount);
      }
      public Task<double> GetCustomerTotalOrdersAmountByBate(Guid customerId, DateTime startDate, DateTime endDate)
      {
            var totalAmount = _dbContext.Orders
                  .Where(o => o.CustomerId == customerId && o.DeletedDate == null && o.CreatedDate >= startDate && o.CreatedDate <= endDate)
                  .Sum(o => o.TotalAmount);
            return Task.FromResult(totalAmount);
      }
      public Task<double> GetNumberOfItemGetFromOneStore(Guid StoreID, DateTime startDate, DateTime endDate)
      {
            var totalItems = _dbContext.OrderItems
                  .Count(oi => oi.StoreId == StoreID && oi.CreatedDate >= startDate && oi.CreatedDate <= endDate && oi.DeletedDate == null);
            return Task.FromResult((double)totalItems);
      }
      public Task<double> GetTotalStaffSellingAmount(Guid staffId, DateTime startDate, DateTime endDate)
      {
            var totalAmount = _dbContext.Orders
                  .Where(o => o.StaffId == staffId && o.DeletedDate == null && o.CreatedDate >= startDate && o.CreatedDate <= endDate)
                  .Sum(o => o.TotalAmount);
            return Task.FromResult(totalAmount);
      }
      public async Task UpdateOrderStatus(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken)
      {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Order with ID {orderId} was not found.");
            order.Status = newStatus;
            order.UpdatedDate = DateTime.UtcNow;
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
      }
      public async Task UpdateOrderItemPdf(Guid orderId, Guid orderItemId, byte[] pdfData, string pdfFileName, CancellationToken cancellationToken)
      {
            var orderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(oi => oi.Id == orderItemId && oi.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Order item with ID {orderItemId} was not found.");
            orderItem.PdfData = pdfData;
            orderItem.PdfFileName = pdfFileName;
            orderItem.UpdatedDate = DateTime.UtcNow;
            _dbContext.OrderItems.Update(orderItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
      }
      public async Task<(byte[] PdfData, string PdfFileName)> GetOrderItemPdf(Guid orderItemId, CancellationToken cancellationToken)
      {
            var orderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(oi => oi.Id == orderItemId && oi.DeletedDate == null, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Order item with ID {orderItemId} was not found.");
            if (orderItem.PdfData == null || orderItem.PdfData.Length == 0)
                  throw new InvalidOperationException($"Order item with ID {orderItemId} does not have an associated PDF.");
            return (orderItem.PdfData, orderItem.PdfFileName ?? $"OrderItem_{orderItemId}.pdf");
      }


}