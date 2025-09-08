using OrganicStore.Dtos;
using OrganicStore.Model;

public interface IOrderService
{
    Task<ServiceResponse<Order>> PlaceOrderAsync(OrderCreateDto dto);
    Task<ServiceResponse<Order>> GetOrderAsync(int id);
    Task<ServiceResponse<IEnumerable<OrderDto>>> GetCustomerOrdersAsync(int customerId);
    Task<ServiceResponse<IEnumerable<OrderDto>>> GetStoreOrdersAsync(int storeId);
    Task<ServiceResponse<Order>> UpdateOrderStatusAsync(int id, string status);
}

