using Microsoft.EntityFrameworkCore;
using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStore.Repositories;
using OrganicStoreApplication.Context;

namespace OrganicStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly DataContext _ctx;

        public OrderService(IOrderRepository orderRepo, DataContext ctx)
        {
            _orderRepo = orderRepo;
            _ctx = ctx;
        }

        public async Task<ServiceResponse<Order>> PlaceOrderAsync(OrderCreateDto dto)
        {
            var response = new ServiceResponse<Order>();

            try
            {
                var store = await _ctx.Stores.FirstOrDefaultAsync(s => s.Id == dto.StoreId);
                if (store == null)
                    return new ServiceResponse<Order> { Success = false, Message = $"Store with Id {dto.StoreId} not found." };

                var customer = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == dto.CustomerId);
                if (customer == null)
                    return new ServiceResponse<Order> { Success = false, Message = $"Customer with Id {dto.CustomerId} not found." };

                var productIds = dto.Items.Select(i => i.ProductId).ToList();
                var products = await _ctx.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

                if (products.Count != productIds.Count)
                {
                    var missing = string.Join(",", productIds.Except(products.Select(p => p.Id)));
                    return new ServiceResponse<Order> { Success = false, Message = $"Missing products: {missing}" };
                }

                var order = new Order
                {
                    CustomerId = dto.CustomerId,
                    StoreId = dto.StoreId,
                    DeliveryAddress = dto.DeliveryAddress,
                    OrderDate = DateTime.UtcNow,
                    Status = "Placed",
                    OrderItems = new List<OrderItem>()
                };

                decimal total = 0m;
                foreach (var item in dto.Items)
                {
                    var prod = products.First(p => p.Id == item.ProductId);

                    if (prod.StoreId != dto.StoreId)
                        return new ServiceResponse<Order> { Success = false, Message = $"Product {prod.Id} does not belong to store {dto.StoreId}." };

                    if (prod.StockQuantity < item.Quantity)
                        return new ServiceResponse<Order> { Success = false, Message = $"Insufficient stock for {prod.Name}." };

                    prod.StockQuantity -= item.Quantity;

                    total += prod.Price * item.Quantity;

                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = prod.Id,
                        Quantity = item.Quantity,
                        Price = prod.Price
                    });
                }

                order.TotalAmount = total;

                await _orderRepo.CreateAsync(order);

                response.Data = order;
                response.Message = "Order placed successfully.";
            }
            catch (DbUpdateException ex)
            {
                response.Success = false;
                response.Message = $"Database update failed: {ex.InnerException?.Message ?? ex.Message}";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Unexpected error: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<Order>> GetOrderAsync(int id)
        {
            var response = new ServiceResponse<Order>();
            var order = await _ctx.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return new ServiceResponse<Order> { Success = false, Message = "Order not found." };

            response.Data = order;
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<OrderDto>>> GetCustomerOrdersAsync(int customerId)
        {
            var orders = await _ctx.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Items = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.OrderItemId, // ✅ FIXED
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                })
                .ToListAsync();

            return new ServiceResponse<IEnumerable<OrderDto>> { Success = true, Data = orders };
        }

        public async Task<ServiceResponse<IEnumerable<OrderDto>>> GetStoreOrdersAsync(int storeId)
        {
            var orders = await _ctx.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.StoreId == storeId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Items = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.OrderItemId, // ✅ FIXED
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                })
                .ToListAsync();

            return new ServiceResponse<IEnumerable<OrderDto>> { Success = true, Data = orders };
        }

        public async Task<ServiceResponse<Order>> UpdateOrderStatusAsync(int id, string status)
        {
            var response = new ServiceResponse<Order>();
            var order = await _orderRepo.GetByIdAsync(id);

            if (order == null)
                return new ServiceResponse<Order> { Success = false, Message = "Order not found." };

            order.Status = status;
            await _orderRepo.UpdateAsync(order);

            response.Data = order;
            response.Message = "Status updated.";
            return response;
        }
    }
}
