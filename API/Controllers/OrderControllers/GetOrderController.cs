using API.Contract.Order;
using API.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OrderControllers;

public partial class OrderController : ControllerBase
{
      [HttpGet("{id:guid}")]
      public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] Guid id)
      {
            try
            {
                  var order = await _orderService.GetOrderById(id);
                  return Ok(order);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving order by ID.");
                  return BadRequest("Can't Get Order " + e.Message);
            }
      }

      [HttpGet]
      public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
      {
            try
            {
                  var orders = await _orderService.GetAllOrders();
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving all orders.");
                  return BadRequest("Can't Get Orders " + e.Message);
            }
      }

      [HttpGet("customer/{customerId:guid}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByCustomerId([FromRoute] Guid customerId)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByCustomerId(customerId);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by customer ID.");
                  return BadRequest("Can't Get Orders By Customer " + e.Message);
            }
      }

      [HttpGet("status/{status}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByStatus([FromRoute] OrderStatus status)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByStatus(status);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by status.");
                  return BadRequest("Can't Get Orders By Status " + e.Message);
            }
      }

      [HttpGet("payment-method/{paymentMethod}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByPaymentMethod([FromRoute] string paymentMethod)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByPaymentMethod(paymentMethod);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by payment method.");
                  return BadRequest("Can't Get Orders By Payment Method " + e.Message);
            }
      }

      [HttpGet("date-range")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByDateRange(startDate, endDate);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by date range.");
                  return BadRequest("Can't Get Orders By Date Range " + e.Message);
            }
      }

      [HttpGet("total-amount-range")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByTotalAmountRange([FromQuery] double min, [FromQuery] double max)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByTotalAmountRange(min, max);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by total amount range.");
                  return BadRequest("Can't Get Orders By Total Amount Range " + e.Message);
            }
      }

      [HttpGet("item/{itemId:guid}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByItemId([FromRoute] Guid itemId)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByItemId(itemId);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by item ID.");
                  return BadRequest("Can't Get Orders By Item " + e.Message);
            }
      }

      [HttpGet("item-serial/{itemSerial}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByItemSerial([FromRoute] string itemSerial)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByItemSerial(itemSerial);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by item serial.");
                  return BadRequest("Can't Get Orders By Item Serial " + e.Message);
            }
      }

      [HttpGet("brand/{brand}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByBrand([FromRoute] string brand)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByBrand(brand);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by brand.");
                  return BadRequest("Can't Get Orders By Brand " + e.Message);
            }
      }

      [HttpGet("model/{model}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByModel([FromRoute] string model)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByModel(model);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by model.");
                  return BadRequest("Can't Get Orders By Model " + e.Message);
            }
      }

      [HttpGet("store-code/{storeCode}")]
      public async Task<ActionResult<List<OrderResponse>>> GetOrdersByStoreCode([FromRoute] string storeCode)
      {
            try
            {
                  var orders = await _orderService.GetOrdersByStoreCode(storeCode);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving orders by store code.");
                  return BadRequest("Can't Get Orders By Store Code " + e.Message);
            }
      }

      [HttpGet("store/{storeId:guid}")]
      public async Task<ActionResult<List<OrderResponse>>> GetStoreOrders([FromRoute] Guid storeId)
      {
            try
            {
                  var orders = await _orderService.GetStoreOrders(storeId);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving store orders.");
                  return BadRequest("Can't Get Store Orders " + e.Message);
            }
      }

      [HttpGet("store/{storeId:guid}/total-amount")]
      public async Task<ActionResult<double>> GetStoreTotalOrdersAmount([FromRoute] Guid storeId)
      {
            try
            {
                  var total = await _orderService.GetStoreTotalOrdersAmount(storeId);
                  return Ok(total);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving store total orders amount.");
                  return BadRequest("Can't Get Store Total Orders Amount " + e.Message);
            }
      }

      [HttpGet("store/{storeId:guid}/total-amount-by-date")]
      public async Task<ActionResult<double>> GetStoreTotalOrdersAmountByDate([FromRoute] Guid storeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
      {
            try
            {
                  var total = await _orderService.GetStoreTotalOrdersAmountByBate(storeId, startDate, endDate);
                  return Ok(total);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving store total orders amount by date.");
                  return BadRequest("Can't Get Store Total Orders Amount By Date " + e.Message);
            }
      }

      [HttpGet("customer/{customerId:guid}/orders")]
      public async Task<ActionResult<List<OrderResponse>>> GetCustomerOrders([FromRoute] Guid customerId)
      {
            try
            {
                  var orders = await _orderService.GetCustomerOrders(customerId);
                  return Ok(orders);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving customer orders.");
                  return BadRequest("Can't Get Customer Orders " + e.Message);
            }
      }

      [HttpGet("customer/{customerId:guid}/total-amount")]
      public async Task<ActionResult<double>> GetCustomerTotalOrdersAmount([FromRoute] Guid customerId)
      {
            try
            {
                  var total = await _orderService.GetCustomerTotalOrdersAmount(customerId);
                  return Ok(total);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving customer total orders amount.");
                  return BadRequest("Can't Get Customer Total Orders Amount " + e.Message);
            }
      }

      [HttpGet("customer/{customerId:guid}/total-amount-by-date")]
      public async Task<ActionResult<double>> GetCustomerTotalOrdersAmountByDate([FromRoute] Guid customerId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
      {
            try
            {
                  var total = await _orderService.GetCustomerTotalOrdersAmountByBate(customerId, startDate, endDate);
                  return Ok(total);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving customer total orders amount by date.");
                  return BadRequest("Can't Get Customer Total Orders Amount By Date " + e.Message);
            }
      }

      [HttpGet("store/{storeId:guid}/item-count")]
      public async Task<ActionResult<double>> GetNumberOfItemGetFromOneStore([FromRoute] Guid storeId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
      {
            try
            {
                  var count = await _orderService.GetNumberOfItemGetFromOneStore(storeId, startDate, endDate);
                  return Ok(count);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving item count from store.");
                  return BadRequest("Can't Get Number Of Items From Store " + e.Message);
            }
      }

      [HttpGet("staff/{staffId:guid}/total-selling-amount")]
      public async Task<ActionResult<double>> GetTotalStaffSellingAmount([FromRoute] Guid staffId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
      {
            try
            {
                  var total = await _orderService.GetTotalStaffSellingAmount(staffId, startDate, endDate);
                  return Ok(total);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Error retrieving total staff selling amount.");
                  return BadRequest("Can't Get Total Staff Selling Amount " + e.Message);
            }
      }
}
