using BLL.DTOs.Order;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// Service for managing orders
    /// Implements Dependency Inversion Principle: depends on IOrderRepository abstraction
    /// Uses factory pattern via Order.Create() static method
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductVariantRepository _productVariantRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IProductVariantRepository productVariantRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productVariantRepository = productVariantRepository;
        }

        /// <summary>
        /// Get all orders with mapping from entity to DTO
        /// </summary>
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersWithItemsAsync();
            return MapOrdersToDto(orders);
        }

        /// <summary>
        /// Get order by ID
        /// </summary>
        public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderWithItemsByIdAsync(orderId);
            if (order == null)
                return null;

            return MapOrderToDto(order);
        }

        /// <summary>
        /// Get orders by customer ID
        /// </summary>
        public async Task<List<OrderDto>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            return MapOrdersToDto(orders);
        }

        /// <summary>
        /// Get orders by status
        /// </summary>
        public async Task<List<OrderDto>> GetOrdersByStatusAsync(string status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return MapOrdersToDto(orders);
        }

        /// <summary>
        /// Create a new order using factory pattern
        /// </summary>
        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            // Factory pattern: Use Order.Create() to instantiate new order
            var newOrder = Order.Create(
                orderCreateDto.CustomerId,
                orderCreateDto.PromotionId,
                orderCreateDto.TotalAmount,
                orderCreateDto.Status);

            // Add order items
            foreach (var itemDto in orderCreateDto.OrderItems)
            {
                var orderItem = OrderItem.Create(
                    newOrder.OrderId,
                    itemDto.ProductVariantId,
                    itemDto.Quantity,
                    itemDto.PriceAtPurchase);

                newOrder.AddOrderItem(orderItem);
            }

            // Save to database
            var createdOrder = await _orderRepository.CreateOrderAsync(newOrder);
            return MapOrderToDto(createdOrder);
        }

        /// <summary>
        /// Update an existing order
        /// </summary>
        public async Task<bool> UpdateOrderAsync(OrderUpdateDto orderUpdateDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderUpdateDto.OrderId);
            if (order == null)
                return false;

            order.Update(orderUpdateDto.PromotionId, orderUpdateDto.TotalAmount, orderUpdateDto.Status);
            return await _orderRepository.UpdateOrderAsync(order);
        }

        /// <summary>
        /// Update order status only
        /// </summary>
        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                return false;

            order.UpdateStatus(status);
            return await _orderRepository.UpdateOrderAsync(order);
        }

        /// <summary>
        /// Delete order (soft delete)
        /// </summary>
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }

        /// <summary>
        /// Search orders with multiple criteria
        /// </summary>
        public async Task<List<OrderDto>> SearchOrdersAsync(int? customerId, string? status, DateTime? fromDate, DateTime? toDate)
        {
            var orders = await _orderRepository.SearchOrdersAsync(customerId, status, fromDate, toDate);
            return MapOrdersToDto(orders);
        }

        #region Mapping Helpers

        /// <summary>
        /// Map Order entity to OrderDto
        /// </summary>
        private OrderDto MapOrderToDto(Order order)
        {
            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                PromotionId = order.PromotionId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                OrderItems = order.OrderItems.Select(oi => MapOrderItemToDto(oi)).ToList()
            };
        }

        /// <summary>
        /// Map OrderItem entity to OrderItemDto
        /// </summary>
        private OrderItemDto MapOrderItemToDto(OrderItem orderItem)
        {
            var productName = orderItem.ProductVariant?.Product?.ProductName ?? "N/A";
            var variantDetails = $"{orderItem.ProductVariant?.Color ?? ""} {orderItem.ProductVariant?.Storage ?? ""}".Trim();

            return new OrderItemDto
            {
                OrderItemId = orderItem.OrderItemId,
                OrderId = orderItem.OrderId,
                ProductVariantId = orderItem.ProductVariantId,
                ProductName = productName,
                ProductVariantDetails = variantDetails,
                Quantity = orderItem.Quantity,
                PriceAtPurchase = orderItem.PriceAtPurchase,
                CreatedAt = orderItem.CreatedAt,
                UpdatedAt = orderItem.UpdatedAt
            };
        }

        /// <summary>
        /// Map list of Order entities to OrderDtos
        /// </summary>
        private List<OrderDto> MapOrdersToDto(List<Order> orders)
        {
            return orders.Select(MapOrderToDto).ToList();
        }

        #endregion
    }
}
